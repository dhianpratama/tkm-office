namespace Core
{
    public interface IStoredProcedure<TResult> : IStoredProcedure
    {
    }

    public interface IStoredProcedure
    {
        string TsqlScriptCreate();
        string TsqlScriptDrop();
    }
}