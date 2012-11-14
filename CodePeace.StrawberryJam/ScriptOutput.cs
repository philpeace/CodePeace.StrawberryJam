using System;

namespace CodePeace.StrawberryJam
{
    public class ScriptOutput : IScriptOutput
    {
        public string ContentType
        {
            get;
            set;
        }

        public string Output
        {
            get;
            set;
        }

        public bool FromCache
        {
            get;
            set;
        }

        public DateTime LastModified
        {
            get;
            set;
        }
    }
}