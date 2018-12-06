using System;
using System.Collections.Generic;
using System.Linq;
using NEMILTEC.Service.Data.Expressions.Data;
using NEMILTEC.Service.Data.Expressions.Query;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.Expressions.Evaluators
{
    public class DataQueryExpressionEvaluator
    {

        #region private shared method

        #endregion


        #region evaluate methods

        #region query
        public object Evaluate(DatabaseQueryExpression expression, object data, IEnumerable<object> parameters = null)
        {
            throw new NotImplementedException();
        }

        //public object Evaluate(DataSetQueryExpression expression, object data, IEnumerable<object> parameters = null)
        //{
        //    IDictionary<string, object> queryParams = null;

        //    if (!parameters.IsNullOrEmpty())
        //    {
        //        var paramArray = parameters.ToArray();
        //        queryParams = (IDictionary<string, object>) paramArray[0];
        //    }
        //    var dataSet = (DataSet)data;
        //    return expression.Query.Execute(dataSet, queryParams);
        //}

        public object Evaluate(DataTableQueryExpression expression, object data, IEnumerable<object> parameters = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region data
        public object Evaluate(DataRowFieldExpression expression, object data, IEnumerable<object> parameters = null)
        {
            return null;
            //var row = (DataRow)parameters;
            //return row.GetCell(expression.ToString()).Value;
        }

        public object Evaluate(DataRowFieldAggregateExpression expression, object data, IEnumerable<object> parameters = null)
        {
            return null;
            //var rows = (DataRow[])parameters;
            //return rows.Select(row => row.GetCell(expression.ToString()).Value).ToArray();
        }


        public object Evaluate(DataEntityPropertyValueExpression expression, object data, IEnumerable<object> parameters = null)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion

        #region ToSQL methods

        //public string ToSQL(QueryExpression expression)
        //{
        //    return ((ISQLObject)expression.Query).ToSQL();
        //}

        public string ToSQL(DataRowFieldExpression expression)
        {
            return expression.EntityName.IsNullOrEmpty() ?
            String.Format("[{0}]", expression.FieldName) :
            String.Format("[{0}].[{1}]", expression.EntityName, expression.FieldName);
        }

        public string ToSQL(DataRowFieldAggregateExpression expression)
        {
            return expression.EntityName.IsNullOrEmpty() ?
            String.Format("[{0}]", expression.FieldName) :
            String.Format("[{0}].[{1}]", expression.EntityName, expression.FieldName);
        }

        public string ToSQL(DataTableQueryExpression expression)
        {
            throw new NotImplementedException();
        }

        public string ToSQL(DataSetQueryExpression expression)
        {
            throw new NotImplementedException();
        }

        public string ToSQL(DatabaseQueryExpression expression)
        {
            throw new NotImplementedException();
        }

        #endregion



    }
}
