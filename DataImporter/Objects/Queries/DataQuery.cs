using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.Objects.Queries
{
    public class DataQuery : ISQLObject
    {

        public Dictionary<string, object> Parameters { get; set; }

        public QueryItems Items { get; set; }


        //#region private methods
        //private DataRow _BuildRow(
        //       DataCell[] parentRowCells,
        //       DataCell[] childRowCells
        //    )
        //{
        //    return new DataRow()
        //    {
        //        Cells = parentRowCells.Union(childRowCells).Distinct()
        //    };
        //}

        //private IEnumerable<DataRow> _Join(DataSet dataSet, string[] columnNames)
        //{
        //    IEnumerable<DataRow> rows = null;

        //    var relColumnNames = dataSet.Relations.SelectMany(r => new[] {r.ChildColumn.FullName, r.ParentColumn.FullName}).ToArray();

        //    foreach (var rel in dataSet.Relations)
        //    {
        //        var parentTable = dataSet.Tables[rel.ParentTable.Name];
        //        var childTable = dataSet.Tables[rel.ChildTable.Name];

        //        var parentColumnName = rel.ParentColumn.FullName;
        //        var childColumnName = rel.ChildColumn.FullName;

        //        if (rows.IsNullOrEmpty())
        //        {
        //            rows = parentTable.Rows.Join(
        //                childTable.Rows,
        //                parentRow => parentRow.GetCell(parentColumnName).Value,
        //                childRow => childRow.GetCell(childColumnName).Value,
        //                (parentRow, childRow) => _BuildRow(
        //                    parentRow.GetCells(columnNames.Union(relColumnNames).Where(c => parentRow.ColumnNames.Contains(c))).ToArray(),
        //                    childRow.GetCells(columnNames.Union(relColumnNames).Where(c => childRow.ColumnNames.Contains(c))).ToArray()
        //                    )
        //                ).ToArray();
        //        }

        //        else
        //        {
        //            if (!rows.Any(r => new [] { parentColumnName, childColumnName}.All(c => r.ColumnNames.Contains(c))))
        //            {
        //                if (rows.Any(r => r.ColumnNames.Contains(parentColumnName)))
        //                {
        //                    rows = rows.Join(
        //                        childTable.Rows,
        //                        parentRow => parentRow.GetCell(parentColumnName).Value,
        //                        childRow => childRow.GetCell(childColumnName).Value,
        //                        (parentRow, childRow) => _BuildRow(
        //                            parentRow.GetCells(columnNames.Union(relColumnNames).Where(c => parentRow.ColumnNames.Contains(c))).ToArray(),
        //                            childRow.GetCells(columnNames.Union(relColumnNames).Where(c => childRow.ColumnNames.Contains(c))).ToArray()
        //                            )
        //                        ).ToArray();
        //                }

        //                else if (rows.Any(r => r.ColumnNames.Contains(childColumnName)))
        //                {
        //                    rows = rows.Join(
        //                        parentTable.Rows,
        //                        parentRow => parentRow.GetCell(childColumnName).Value,
        //                        childRow => childRow.GetCell(parentColumnName).Value,
        //                        (parentRow, childRow) => _BuildRow(
        //                            parentRow.GetCells(columnNames.Union(relColumnNames).Where(c => parentRow.ColumnNames.Contains(c))).ToArray(),
        //                            childRow.GetCells(columnNames.Union(relColumnNames).Where(c => childRow.ColumnNames.Contains(c))).ToArray()
        //                            )
        //                        ).ToArray();
        //                }
        //            }
        //        }
        //    }

        //    return rows.Select(r => r.ProjectColumns(columnNames)).ToArray();
        //}


        //private IEnumerable<DataRow> _ExecuteQuery(IEnumerable<DataRow> rows)
        //{

        //    var columnNames = Items.Projections.Select(p => p.ToString());

        //    #region filter rows by condition - SQL WHERE
        //    if (Items.Condition != null)
        //    {
        //        rows = rows.Where(r => Convert.ToBoolean(Items.Condition.Evaluate(r))).ToArray();
        //    }
        //    #endregion

        //    #region group rows/execute aggregates - SQL GROUP BY
        //    if (!Items.Groupings.IsNullOrEmpty() && !Items.Aggregates.IsNullOrEmpty())
        //    {
        //        //group rows by grouping fields
        //        var grouping = rows.GroupBy(row =>
        //                    new KeyValuePairEqualityComparer()
        //                    {
        //                        KeyValuePair = Items.Groupings.Select(g => new { Key = g.ToString(), Value = row.GetCell(g.ToString()).Value })
        //                        .ToDictionary(g => g.Key, g => g.Value)
        //                    }
        //        );

        //        //execute aggregates on each row group + combine with grouping fields 
        //        //result is rows with columns group1 field, group2 field, ..., groupN field, aggregate1 field, aggregate2 field, ..., aggregateN field
        //        //and values group1 value, group2 value, ... groupN value, aggregate1 result, aggregate2 result, ..., aggregateN result
        //        rows = grouping.Select(g =>
        //           DataRow.From(
        //                g.Key.KeyValuePair.Union(
        //                    Items.Aggregates.Select(
        //                    a => new {
        //                        Key = a.ToString(),
        //                        Value = a.Expression.Evaluate(g.ToArray())
        //                    })
        //                    .ToDictionary(a => a.Key, a => a.Value)
        //                 ).ToDictionary(a => a.Key, a => a.Value)
        //            )
        //        ).ToArray();

        //        #region filter by aggregate condition - SQL HAVING
        //        if (Items.AggregateCondition != null)
        //        {
        //            rows = rows.Where(r => Convert.ToBoolean(Items.AggregateCondition.Evaluate(r))).ToArray();
        //        }
        //        #endregion

        //    }
        //    #endregion

        //    #region order rows - SQL ORDER BY
        //    if (!Items.Orderings.IsNullOrEmpty())
        //    {
        //        rows = rows.OrderBy(row => new KeyValuePairEqualityComparer()
        //        {
        //            KeyValuePair = Items.Orderings.Select(o => o.ToString())
        //            .ToDictionary(o => o, o => row.GetCell(o).Value)
        //        }).ToArray();
        //    }
        //    #endregion

        //    return rows;
        //}

        //#endregion

        #region interface methods
        //public QueryResult Execute(string databaseName, IDictionary<string, object> parameters)
        //{
        //    return new QueryResult();
        //}


        //public object Execute(DataSet dataSet, IDictionary<string, object> parameters)
        //{

        //    DataRow[] rows = null;

        //    var columnNames = Items.Projections.Select(p => p.ToString()).ToArray();

        //    #region join tables - SQL JOIN

        //    rows = _Join(dataSet, columnNames).ToArray();

        //    #endregion

        //    //update parameters

        //    return _ExecuteQuery(rows);
        //}

        //public object Execute(DataTable data, IDictionary<string, object> parameters)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

        private string _GetFieldParameterSQL(DataQueryFieldParameter fieldParam)
        {
            return fieldParam.Expression != null ? ((ISQLExpression)fieldParam.Expression).ToSQL() : fieldParam.ToString();
        }

        public string ToSQL()
        {
            var strBuilder = new StringBuilder();

            if (!Items.Projections.IsNullOrEmpty() && Items.Aggregates.IsNullOrEmpty())
            {
                strBuilder.AppendFormat("SELECT {0} FROM [{1}]\n", Items.Projections.Select(p => _GetFieldParameterSQL(p)).ToCSV(), Items.EntityName);
            }

            else if (Items.Projections.IsNullOrEmpty() && !Items.Aggregates.IsNullOrEmpty())
            {
                strBuilder.AppendFormat("SELECT {0} FROM [{1}]\n", Items.Aggregates.Select(a => _GetFieldParameterSQL(a)).ToCSV(), Items.EntityName);
            }

            if (!Items.Joins.IsNullOrEmpty())
            {                                           
                foreach (var rel in Items.Joins)
                {
                    strBuilder.AppendFormat(
                    "{0} JOIN [{1}] ON [{2}].[{3}] = [{4}].[{5}]\n",
                    rel.ToString(),
                    rel.ChildTableName,
                    rel.ParentTableName,
                    rel.ParentColumnName,
                    rel.ChildTableName,
                    rel.ChildColumnName);
                }
            }

            if (Items.Condition != null)
            {
                strBuilder.AppendFormat("WHERE {0}\n", ((ISQLExpression) Items.Condition).ToSQL());
            }

            if (!Items.Groupings.IsNullOrEmpty())
            {
                strBuilder.AppendFormat("GROUP BY {0}\n", Items.Groupings.Select(g => _GetFieldParameterSQL(g)).ToCSV());
                if (Items.AggregateCondition != null)
                {
                    strBuilder.AppendFormat("HAVING {0}\n", ((ISQLExpression)Items.AggregateCondition).ToSQL());
                }
            }

            if (!Items.Orderings.IsNullOrEmpty())
            {
                strBuilder.AppendFormat("ORDER BY {0}", Items.Orderings.Select(o => String.Format("{0} {1}", _GetFieldParameterSQL(o), o.ToString())).ToCSV());
            }

            return strBuilder.ToString();

        }
    }
}
