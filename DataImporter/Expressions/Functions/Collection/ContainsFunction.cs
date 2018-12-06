using System.Collections.Generic;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Service.Data.Expressions.Evaluators;

namespace NEMILTEC.Service.Data.Expressions.Functions.Collection
{
    public class ContainsFunction : AFunctionExpression
    {


        public override object Evaluate(object data, IEnumerable<object> parameters = null)
        {
            return new FunctionExpressionEvaluator().Evaluate(this, data, parameters);
        }

    }
}
