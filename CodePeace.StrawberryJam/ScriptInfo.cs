using System;
using System.Diagnostics;
using System.Text;

namespace CodePeace.StrawberryJam
{
    public class ScriptInfo : IScriptInfo
    {
        public ScriptInfo(string url, string localPath, string cdnPath, ScriptType scriptType, bool siteWide = false, string area = null)
        {
            Url = url;
            LocalPath = localPath;
            CDNPath = cdnPath;
            ScriptType = scriptType;
            SiteWide = siteWide;
            Area = area;

            UniqueKey = localPath;

            if (UniqueKey == null)
            {
                throw new ArgumentNullException("localPath");
            }
        }

        public ScriptInfo(string source, string cdnPath, ScriptType scriptType, string key, bool siteWide = false, string area = null)
        {
            IsInline = true;
            Source = source;
            CDNPath = cdnPath;
            ScriptType = scriptType;
            SiteWide = siteWide;
            Area = area;
            LastModified = DateTime.Today;

            var hash = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(Source));
            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            var s = sb.ToString();
            LocalPath = "/local/" + s;

            UniqueKey = key;

            Trace.WriteLine(area + "." + key + " " + s);

            if (UniqueKey == null)
            {
                throw new ArgumentNullException("key");
            }
        }

        private DateTime _lastModified;

        public int ItemOrder
        {
            get;
            set;
        }

        public bool IsInline
        {
            get;
            set;
        }

        public string Source
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public string UniqueKey
        {
            get;
            set;
        }

        public string LocalPath
        {
            get;
            set;
        }

        public string Area
        {
            get;
            set;
        }

        public string CDNPath
        {
            get;
            set;
        }

        public ScriptType ScriptType
        {
            get;
            set;
        }

        public bool SiteWide
        {
            get;
            set;
        }

        public DateTime LastModified
        {
            get
            {
                if (IsInline)
                {
                    return _lastModified;
                }
                else
                {
                    _lastModified = System.IO.File.GetLastWriteTime(LocalPath);
                    return _lastModified;
                }
            }
            set
            {
                _lastModified = value;
            }
        }
    }
}