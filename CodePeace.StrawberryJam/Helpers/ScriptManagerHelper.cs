﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;

namespace CodePeace.StrawberryJam.Helpers
{
    public static class ScriptManagerHelper
    {
        public static MvcHtmlString AddCss(this HtmlHelper helper, string localPath, string area = null, string cdnPath = null, bool siteWide = false)
        {
            var scriptType = ScriptType.Stylesheet;

            var info = new ScriptInfo(localPath, helper.ViewContext.HttpContext.Server.MapPath(localPath).Replace(@"/", @"\"), cdnPath, scriptType, siteWide, area);

            var manager = ServiceLocator.Current.GetInstance<IScriptManager>();

            manager.Add(info);

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString AddJavaScript(this HtmlHelper helper, string localPath, string area = null, string cdnPath = null, bool siteWide = false)
        {
            var scriptType = ScriptType.JavaScript;

            var info = new ScriptInfo(localPath, helper.ViewContext.HttpContext.Server.MapPath(localPath).Replace(@"/", @"\"), cdnPath, scriptType, siteWide, area);

            var manager = ServiceLocator.Current.GetInstance<IScriptManager>();

            manager.Add(info);

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString AddScriptSource(this HtmlHelper helper, Func<dynamic, HelperResult> source, string key, string area = null, string cdnPath = null, bool siteWide = false)
        {
            var scriptType = ScriptType.JavaScript;

            var info = new ScriptInfo(source(null).ToHtmlString(), cdnPath, scriptType, key, siteWide, area);

            var manager = ServiceLocator.Current.GetInstance<IScriptManager>();

            manager.Add(info);

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString WriteScripts(this HtmlHelper htmlHelper, string area = null)
        {
            const ScriptType type = ScriptType.JavaScript;

            bool concatenate = bool.Parse(ConfigurationManager.AppSettings["ScriptManager.Concatenate"]);

            if (concatenate)
            {
                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                string url = urlHelper.Content("~/ScriptManager/" + type.ToString() + "/" + area);

                var builder = new TagBuilder("script");
                builder.Attributes.Add("type", "text/javascript");
                builder.Attributes.Add("src", url);

                return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
            }
            else
            {
                var manager = ServiceLocator.Current.GetInstance<IScriptManager>();

                string output = "";

                foreach (var item in manager.ListForType(type).ScriptsForArea(area))
                {
                    var builder = new TagBuilder("script");
                    builder.Attributes.Add("type", "text/javascript");

                    if (!item.IsInline)
                    {
                        builder.Attributes.Add("src", item.Url);
                    }
                    else
                    {
                        builder.InnerHtml = String.Format("/* {0} */\n", item.UniqueKey) + item.Source;
                    }

                    output += new MvcHtmlString(builder.ToString(TagRenderMode.Normal)) + "\n";
                }

                return new MvcHtmlString(output);
            }
        }

        public static MvcHtmlString WriteStyles(this HtmlHelper htmlHelper, string area = null)
        {
            var type = ScriptType.Stylesheet;

            bool concatenate = bool.Parse(ConfigurationManager.AppSettings["ScriptManager.Concatenate"]);

            if (concatenate)
            {
                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                string url = urlHelper.Content("~/ScriptManager/" + type.ToString() + "/" + area);

                var builder = new TagBuilder("link");
                builder.Attributes.Add("type", "text/css");
                builder.Attributes.Add("rel", "stylesheet");
                builder.Attributes.Add("href", url);

                return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
            }
            else
            {
                var manager = ServiceLocator.Current.GetInstance<IScriptManager>();

                string output = "";

                foreach (var item in manager.ListForType(type).ScriptsForArea(area))
                {
                    if (!item.IsInline)
                    {
                        var builder = new TagBuilder("link");
                        builder.Attributes.Add("type", "text/css");
                        builder.Attributes.Add("rel", "stylesheet");
                        builder.Attributes.Add("href", item.Url);

                        output += builder.ToString(TagRenderMode.SelfClosing) + "\n";
                    }
                    else
                    {
                        var builder = new TagBuilder("style");
                        builder.Attributes.Add("type", "text/css");
                        builder.InnerHtml = item.Source;
                        output += builder.ToString(TagRenderMode.Normal) + "\n";
                    }
                }

                return new MvcHtmlString(output);
            }
        }
    }

}