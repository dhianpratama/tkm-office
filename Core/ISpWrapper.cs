using System.Collections.Generic;

namespace Core
{
    public interface ISpWrapper
    {
        IEnumerable<TResult> ExecuteQueryStoredProcedure<TResult>(IStoredProcedure<TResult> procedure);
        int ExecuteNonQueryStoredProcedure(IStoredProcedure procedure);
    }
}