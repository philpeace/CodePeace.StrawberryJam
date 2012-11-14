using System;

namespace CodePeace.StrawberryJam
{
    public interface IScriptOutput
    {
        string ContentType
        {
            get;
            set;
        }

        string Output
        {
            get;
            set;
        }

        bool FromCache
        {
            get;
            set;
        }

        DateTime LastModified
        {
            get;
            set;
        }
    }
}