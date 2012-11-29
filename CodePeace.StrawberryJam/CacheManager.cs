using System;
using System.Web;
using System.Web.Caching;
using CodePeace.Common;

namespace CodePeace.StrawberryJam
{
    public class CacheManager : ICacheManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CacheManager()
            : this(new HttpContextAccessor())
        {
            
        }

        public CacheManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public T Get<T>(string appKey, Func<T> func)
        {
            var cache = _contextAccessor.Current().Cache;
            T o;

            if (cache[appKey] == null)
            {
                o = func();
                cache[appKey] = o;
            }
            else
            {
                o = (T)cache[appKey];
            }

            return o;
        }
    }
}