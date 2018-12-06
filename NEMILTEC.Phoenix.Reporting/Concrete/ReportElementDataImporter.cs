using System.Collections.Generic;
using NEMILTEC.Service.Reporting.Abstract;
using NEMILTEC.Service.Reporting.Concrete.Elements;
using NEMILTEC.Shared.Classes.Data;
using NEMILTEC.Service.Data.Database;
using NEMILTEC.Interfaces.Service.Data.Database;
using NEMILTEC.Interfaces.Service.Reporting;

namespace NEMILTEC.Service.Reporting.Concrete
{
    public static class ReportElementDataImporter
    {
        private static object _EvaluateExpression(IReportElement element)
        {
            return element.Expression.Evaluate(element.Data);
        }

        public static object Import(TableReportElement element, string connectionString, IDictionary<string, object> parameters = null)
        {
            if (element.Expression != null)
            {
                return _EvaluateExpression(element);
            }

            IDatabase<DataTable> db = new Database() {ConnectionString = connectionString};
            string sql = element.Query.ToSQL();

            var dataTable = db.ExecuteQuery(sql);

            return dataTable;
        }

        public static object Import(ChartReportElement element, string connectionString, IDictionary<string, object> parameters = null)
        {
            return null;
        }

        public static object Import(TextReportElement element, string connectionString, IDictionary<string, object> parameters = null)
        {
            if (element.Expression != null)
            {
                return _EvaluateExpression(element);
            }

            IDatabase<DataTable> db = new Database() { ConnectionString = connectionString };
            string sql = element.Query.ToSQL();

            var result = db.ExecuteScalar(sql);

            return result;
        }
    }
}
