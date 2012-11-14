using System.Collections.Generic;

namespace CodePeace.StrawberryJam
{
    public interface IScriptFileList
    {
        IEnumerable<IScriptInfo> Scripts
        {
            get;
        }

        void Add(IScriptInfo info);

        IEnumerable<IScriptInfo> ScriptsForArea(string area = null);
    }
}