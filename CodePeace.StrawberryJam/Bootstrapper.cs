using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;

namespace CodePeace.StrawberryJam
{
    public class Bootstrapper
    {
        public static void Init()
        {
            BuildManager.AddReferencedAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
