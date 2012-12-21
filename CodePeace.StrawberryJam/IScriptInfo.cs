using System;

namespace CodePeace.StrawberryJam
{
    public interface IScriptInfo
    {
        int ItemOrder
        {
            get;
            set;
        }

        bool IsInline
        {
            get;
            set;
        }

        string Source
        {
            get;
            set;
        }

        string Url
        {
            get;
            set;
        }

        string UniqueKey
        {
            get;
            set;
        }

        string LocalPath
        {
            get;
            set;
        }

        string Area
        {
            get;
            set;
        }

        ScriptType ScriptType
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