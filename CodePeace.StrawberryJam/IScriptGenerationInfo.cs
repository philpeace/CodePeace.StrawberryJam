namespace CodePeace.StrawberryJam
{
    public interface IScriptGenerationInfo
    {
        ScriptType Type
        {
            get;
            set;
        }

        string Area
        {
            get;
            set;
        } 
    }
}