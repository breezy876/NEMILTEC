using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;

namespace NEMILTEC.Service.Data.Objects.Queries
{
    public class QueryItems
    {
        public string EntityName { get; set; }

        /// <summary>
        /// condition expression - SQL WHERE clause
        /// </summary>
        public IDataExpression Condition { get; set; }

        /// <summary>
        /// aggregate condition - SQL HAVING clause
        /// </summary>
        public IDataExpression AggregateCondition { get; set; }

        /// <summary>
        /// aggregate expressions - SQL SUM, AVG, COUNT
        /// </summary>
        public IEnumerable<DataQueryFieldParameter> Aggregates { get; set; }

        /// <summary>
        /// field projections - SQL SELECT (x,y,z...) FROM table
        /// </summary>
        public IEnumerable<DataQueryFieldParameter> Projections { get; set; }

        /// <summary>
        /// for grouping result - SQL GROUP BY clause
        /// </summary>
        public IEnumerable<DataQueryFieldParameter> Groupings { get; set; }

        /// <summary>
        /// for ordering result - SQL ORDER BY clause
        /// </summary>
        public IEnumerable<DataQueryOrderingParameter> Orderings { get; set; }

        /// <summary>
        /// table/field relations - SQL JOIN
        /// </summary>
        public IEnumerable<DataQueryJoinParameter> Joins { get; set; }
    }
}
