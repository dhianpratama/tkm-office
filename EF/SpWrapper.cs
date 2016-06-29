using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace EF
{
    public class SpWrapper : ISpWrapper
    {
        private readonly SmartShelveContext _context;

        public SpWrapper(SmartShelveContext context)
        {
            _context = context;
        }

        public IEnumerable<TResult> ExecuteQueryStoredProcedure<TResult>(IStoredProcedure<TResult> procedure)
        {
            var parameters = CreateSqlParametersFromProperties(procedure);
            var format = CreateSpCommand<TResult>(parameters, procedure);
            return _context.Database.SqlQuery<TResult>(format, parameters.Cast<object>().ToArray());
        }

        private static List<SqlParameter> CreateSqlParametersFromProperties<TResult>(IStoredProcedure<TResult> procedure)
        {
            var procedureType = procedure.GetType();
            var propertiesOfProcedure = procedureType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var parameters = propertiesOfProcedure
                .Select(propertyInfo =>
                    new SqlParameter(
                        string.Format("@{0}", (object)propertyInfo.Name),
                        propertyInfo.GetValue(procedure, new object[] { }))
                )
                .ToList();
            return parameters;
        }

        private static string CreateSpCommand<TResult>(List<SqlParameter> parameters, IStoredProcedure<TResult> procedure)
        {
            var spName = procedure.GetType().Name;
            var queryString = string.Format("{0}", spName);

            parameters.ForEach(x => queryString = string.Format("{0} {1},", queryString, x.ParameterName));

            return queryString.TrimEnd(',');
        }

        public int ExecuteNonQueryStoredProcedure(IStoredProcedure procedure)
        {
            var parameters = CreateSqlParametersFromProperties(procedure);
            var tSql = CreateSpCommand(parameters, procedure);
            return _context.Database.ExecuteSqlCommand(tSql, parameters.Cast<object>().ToArray());
        }

        private static List<SqlParameter> CreateSqlParametersFromProperties(IStoredProcedure procedure)
        {
            var procedureType = procedure.GetType();
            var propertiesOfProcedure = procedureType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (!propertiesOfProcedure.Any())
                return null;

            var parameters = propertiesOfProcedure
                .Select(propertyInfo =>
                    new SqlParameter(
                        string.Format("@{0}", (object)propertyInfo.Name),
                        propertyInfo.GetValue(procedure, new object[] { }))
                )
                .ToList();
            return parameters;
        }

        private static string CreateSpCommand(List<SqlParameter> parameters, IStoredProcedure procedure)
        {
            var spName = procedure.GetType().Name;
            var queryString = string.Format("{0}", spName);

            if (parameters != null)
                parameters.ForEach(x => queryString = string.Format("{0} {1},", queryString, x.ParameterName));

            return queryString.TrimEnd(',');
        }
    }
}
