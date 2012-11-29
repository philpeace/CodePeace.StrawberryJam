using System;

namespace CodePeace.StrawberryJam
{
    public interface ICacheManager
    {
        T Get<T>(string appKey, Func<T> func);
    }
}