using System.Collections.Generic;

namespace CodePeace.StrawberryJam
{
    public class ScriptInfoComparer : IEqualityComparer<IScriptInfo>
    {
        public bool Equals(IScriptInfo x, IScriptInfo y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        public int GetHashCode(IScriptInfo obj)
        {
            return (obj.UniqueKey + obj.ScriptType.ToString()).GetHashCode();
        }
    }
}