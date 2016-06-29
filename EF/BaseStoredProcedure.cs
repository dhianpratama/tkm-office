using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace EF
{
    public class BaseStoredProcedure : IStoredProcedure
    {
        public string TsqlScriptCreate()
        {
            return File.ReadAllText(string.Format("{0}/../../SqlScript/{1}Create.sql", AppDomain.CurrentDomain.BaseDirectory, GetType().Name));
        }

        public string TsqlScriptDrop()
        {
            return File.ReadAllText(string.Format("{0}/../../SqlScript/{1}Drop.sql", AppDomain.CurrentDomain.BaseDirectory, GetType().Name));
        }
    }

    public class BaseStoredProcedure<TResult> : BaseStoredProcedure, IStoredProcedure<TResult>
    {
    }
}


