using System.Web;

namespace CodePeace.Common
{
    public interface IHttpContextAccessor
    {
        HttpContextBase Current();
    }
}