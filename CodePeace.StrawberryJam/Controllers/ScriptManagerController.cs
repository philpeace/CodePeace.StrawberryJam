using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodePeace.StrawberryJam.Attributes;

namespace CodePeace.StrawberryJam.Controllers
{
    [EnableCompression]
    public class ScriptManagerController : Controller
    {
        public ScriptManagerController()
            : this(new ScriptManager())
        {
            
        }

        public ScriptManagerController(IScriptManager scriptManager)
        {
            _scriptManager = scriptManager;
        }

        private readonly IScriptManager _scriptManager;

        public ActionResult Output(ScriptGenerationInfo vm)
        {
            var output = _scriptManager.Generate(vm);

            if (IsClientCached(output.LastModified))
            {
                return new HttpStatusCodeResult(304, "Not Modified");
            }
            else
            {
                Response.Cache.SetLastModified(output.LastModified);
                return Content(output.Output, output.ContentType);
            }
        }

        private bool IsClientCached(DateTime contentModified)
        {
            string header = Request.Headers["If-Modified-Since"];

            if (header != null)
            {
                DateTime ifModifiedSince;
                if (DateTime.TryParse(header, out ifModifiedSince))
                {
                    ifModifiedSince = ifModifiedSince.AddMilliseconds(-1 * ifModifiedSince.Millisecond);
                    var isClientCached = ifModifiedSince.Subtract(contentModified).Seconds == 0;
                    return isClientCached;
                }
            }

            return false;
        }
    }
}
