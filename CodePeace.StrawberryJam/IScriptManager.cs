using System.Collections.Generic;

namespace CodePeace.StrawberryJam
{
    public interface IScriptManager
    {
        IScriptOutput Generate(IScriptGenerationInfo info);

        string GetHash(IEnumerable<IScriptInfo> scripts);

        IScriptFileList ListForType(ScriptType type);

        void Add(IScriptInfo info);

        void Clear();
    }
}