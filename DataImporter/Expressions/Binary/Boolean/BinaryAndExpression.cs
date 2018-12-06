using System.Collections.Generic;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Service.Data.Expressions.Evaluators;

namespace NEMILTEC.Service.Data.Expressions.Binary.Boolean
{
    public class BinaryAndExpression : ABinaryExpression
    {

        public override object Evaluate(object data, IEnumerable<object> parameters)
        {
            return new BinaryExpressionEvaluator().Evaluate(this, data, parameters);
        }

        public override string ToSQL()
        {
            return new BinaryExpressionEvaluator().ToSQL(this);
        }
    }
}
