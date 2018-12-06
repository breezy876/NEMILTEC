using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Service.Data.Expressions.Evaluators;

namespace NEMILTEC.Service.Data.Expressions.Query
{
    /// <summary>
    /// in-memory dynamic abstract query interface
    /// allows user to construct queries and execute them against a data object - any in-memory representation of relational data e.g DataSet
    /// </summary>
    public class DataSetQueryExpression : ADataExpression, ISQLExpression
    {
        public IDataQuery Query { get; set; }


        public string ToSQL()
        {
            return new DataQueryExpressionEvaluator().ToSQL(this);
        }

        public override object Evaluate(object data, IEnumerable<object> parameters = null)
        {
            return null;
            //return new DataQueryExpressionEvaluator().Evaluate(this, data, parameters);
        }

    }
}
