using System;
using System.Web;

namespace CodePeace.Common
{
    public class HttpContextAccessor : IHttpContextAccessor
    {
        public HttpContextBase Current()
        {
            var httpContext = GetStaticProperty();
            if (httpContext == null)
                return null;
            return new HttpContextWrapper(httpContext);
        }

        private HttpContext GetStaticProperty()
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return null;
            }

            try
            {
                if (httpContext.Request == null)
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return httpContext;
        }
    }
}