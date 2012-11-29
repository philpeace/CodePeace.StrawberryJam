using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CodePeace.StrawberryJam
{
    public class JamAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CodePeace.StrawberryJam";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MyPlugin1_default",
                "ScriptManager/{type}/{area}",
                new
                {
                    controller = "ScriptManager",
                    action = "Output",
                    area = UrlParameter.Optional
                }
                );
        }
    }
}
