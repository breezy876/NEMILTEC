using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Service.Data.Expressions.Evaluators;

namespace NEMILTEC.Service.Data.Expressions.Functions.Aggregate
{
    public class MaxFunction : AFunctionExpression
    {

        public new IEnumerable<ISQLExpression> Expressions { get; set; }

        private ISQLExpression[] _expressions;

        public override object Evaluate(object data, IEnumerable<object> parameters)
        {
            return new FunctionExpressionEvaluator().Evaluate(this, data, parameters);
        }

        public string ToSQL()
        {
            return new FunctionExpressionEvaluator().ToSQL(this);
        }
    }
}
