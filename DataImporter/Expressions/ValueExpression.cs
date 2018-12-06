using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Service.Data.Expressions.Abstract;

namespace NEMILTEC.Service.Data.Expressions
{
    public class ValueExpression : ADataExpression, ISQLExpression
    {

        public object Value { get; set; }

        public override object Evaluate(object data, IEnumerable<object> parameters)
        {
            return Value ?? parameters;
        }

        public string ToSQL()
        {
            return ValueExpressionSQLConverter.Convert(Value);
        }
    }
}
