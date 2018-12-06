using System;
using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Service.Data.Expressions.Evaluators;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Expressions.Data
{

    public class DataRowFieldAggregateExpression : ADataExpression, ISQLExpression
    {


        public string EntityName { get; set; }
        public string FieldName { get; set; }

        public override object Evaluate(object data, IEnumerable<object> parameters)
        {
            return new DataQueryExpressionEvaluator().Evaluate(this, data, parameters);
        }

        public string ToSQL()
        {
            return new DataQueryExpressionEvaluator().ToSQL(this);
        }

        public override string ToString()
        {
            return EntityName.IsNullOrEmpty() ?
            String.Format("{0}", FieldName) :
            String.Format("{0}.{1}", EntityName, FieldName);
        }
    }
}
