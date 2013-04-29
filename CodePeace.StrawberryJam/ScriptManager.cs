using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using CodePeace.Common;
using CodePeace.Common.Extensions;

namespace CodePeace.StrawberryJam
{
    public class ScriptManager : IScriptManager
    {
        public ScriptManager()
            : this(new CacheManager(), new HttpContextAccessor())
        {
            
        }

        public ScriptManager(ICacheManager cacheManager, IHttpContextAccessor contextAccessor)
        {
            _cacheManager = cacheManager;
            _context = contextAccessor.Current();
        }

        private readonly ICacheManager _cacheManager;
        private readonly HttpContextBase _context;

        public IScriptFileList ListForType(ScriptType type)
        {
            string key = CreateKey(type);

            var list = (_context.Application[key] as IScriptFileList) ?? new ScriptFileList();

            return list;
        }

        private static string CreateKey(ScriptType type)
        {
            return "__sm__{0}".ToFormat(type.ToString());
        }

        private void SaveList(ScriptType type, IScriptFileList list)
        {
            string key = CreateKey(type);
            _context.Application[key] = list;
        }

        public IScriptOutput Generate(IScriptGenerationInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            var output = new ScriptOutput();

            switch (info.Type)
            {
                case ScriptType.JavaScript:
                    output.ContentType = @"application/javascript";
                    break;
                case ScriptType.Stylesheet:
                    output.ContentType = @"text/css";
                    break;
            }

            IScriptFileList list = ListForType(info.Type);

            IEnumerable<IScriptInfo> scripts = list.ScriptsForArea(info.Area);

            var scriptInfos = scripts as IScriptInfo[] ?? scripts.ToArray();
            var hash = GetHash(scriptInfos);
            var appKey = CreateKey(info.Type) + hash;

            output.FromCache = true;
            output.LastModified = scriptInfos.Any() ? scriptInfos.Max(s => s.LastModified) : DateTime.MinValue;

            output.Output = _cacheManager.Get(appKey, () =>
            {
                output.FromCache = false;
                return Merge(info, scriptInfos);
            });

            return output;
        }

        private string Merge(IScriptGenerationInfo info, IEnumerable<IScriptInfo> scripts)
        {
            var scriptbody = new StringBuilder();
            scriptbody.AppendFormat("/* Generated: {0} */\n", DateTime.Now.ToString());

            var scriptsToRender = scripts;
            var minify = bool.Parse(ConfigurationManager.AppSettings["SJ.Compress"]);

            foreach (var script in scriptsToRender)
            {
                if (!script.IsInline && !string.IsNullOrWhiteSpace(script.LocalPath))
                {
                    using (var fs = new FileStream(script.LocalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var file = new StreamReader(fs))
                        {
                            var fileContent = file.ReadToEnd();

                            if (info.Type == ScriptType.Stylesheet)
                            {
                                var fromUri = new Uri(HttpContext.Current.Server.MapPath("~/"));
                                var toUri = new Uri(new FileInfo(script.LocalPath).DirectoryName);

                                string imageUrlRoot = (HttpContext.Current.Request.ApplicationPath.EndsWith("/")) ? HttpContext.Current.Request.ApplicationPath : HttpContext.Current.Request.ApplicationPath + "/";
                                imageUrlRoot += fromUri.MakeRelativeUri(toUri).ToString();
                                fileContent = fileContent.Replace("url(\"images", "url(\"" + imageUrlRoot + "/images");
                            }

                            if (!minify)
                            {
                                scriptbody.AppendLine(String.Format("/* {0} */\n", script.Url));
                            }

                            scriptbody.AppendLine(fileContent);
                            file.Close();
                        }
                    }
                }
                else if (script.IsInline)
                {
                    scriptbody.AppendLine(String.Format("/* {0} */\n", script.UniqueKey));
                    scriptbody.AppendLine(script.Source);
                }
            }

            string scriptOutput = scriptbody.ToString();

            if (minify)
            {
                switch (info.Type)
                {
                    case ScriptType.JavaScript:
                        var jsCompressor = new Yahoo.Yui.Compressor.JavaScriptCompressor();
                        scriptOutput = jsCompressor.Compress(scriptOutput);
                        break;
                    case ScriptType.Stylesheet:
                        var cssCompressor = new Yahoo.Yui.Compressor.CssCompressor();
                        scriptOutput = cssCompressor.Compress(scriptOutput);
                        break;
                }
            }

            return scriptOutput;
        }

        public string GetHash(IEnumerable<IScriptInfo> scripts)
        {
            var input = scripts.Distinct(new ScriptInfoComparer()).OrderBy(s => s.LocalPath).Aggregate(string.Empty, (a, s) => a + s.UniqueKey + "|" + s.LastModified.ToString());
            var hash = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));

            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public void Add(IScriptInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            IScriptFileList list = ListForType(info.ScriptType);

            list.Add(info);

            SaveList(info.ScriptType, list);
        }

        public void Clear()
        {
            foreach (var type in Enum.GetValues(typeof(ScriptType)))
            {
                string key = CreateKey((ScriptType)type);
                _context.Application[key] = new ScriptFileList();
            }
        }
    }
}