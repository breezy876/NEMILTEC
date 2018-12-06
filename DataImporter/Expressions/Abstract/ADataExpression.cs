using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;

namespace NEMILTEC.Service.Data.Expressions.Abstract
{
    /// <summary>
    /// a data expression 
    /// for evaluating/parsing data expressions e.g rules in IntelliFlows
    /// 
    /// author: chris brown
    /// date created: 01/07/2015
    /// </summary>
    /// 

    public abstract class ADataExpression : IDataExpression
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }

        /// <summary>
        /// evaluates expression and yields result
        /// </summary>
        /// <returns></returns>
        public abstract object Evaluate(object data, IEnumerable<object> parameters = null);

    }
}
