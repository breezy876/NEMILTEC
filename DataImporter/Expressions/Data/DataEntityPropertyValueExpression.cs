using System.Collections.Generic;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Service.Data.Expressions.Evaluators;

namespace NEMILTEC.Service.Data.Expressions.Data
{
    public class DataEntityPropertyValueExpression : ADataExpression
    {

        public string EntityName { get; set; }
        public string FieldName { get; set; }

        public override object Evaluate(object data, IEnumerable<object> parameters = null)
        {
            return new DataQueryExpressionEvaluator().Evaluate(this, data, parameters);
        }
    }
}
