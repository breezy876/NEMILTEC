using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Data.Objects;

namespace NEMILTEC.Service.Data.Objects.Queries
{
    public static class QueryFactory
    {
        public static ISQLObject Create(Domain.Query query)
        {
            var queryObj = new DataQuery()
            {

                Items = new QueryItems()
                {
                    EntityName = query.TableName,
                    Projections = query.QueryProjections.Select(p => new DataQueryFieldParameter()
                    {
                        Name = p.Name,
                        EntityName = p.TableName,
                        FieldName = p.ColumnName
                    }),
                    Joins = query.QueryJoins.Select(j => new DataQueryJoinParameter()
                    {
                        Type = j.QueryJoinType != null ? (JoinType)j.QueryJoinType.Id : JoinType.Inner,
                        ParentColumnName = j.ParentColumnName,
                        ChildColumnName = j.ChildColumnName,
                        ParentTableName = j.ParentTableName,
                        ChildTableName = j.ChildTableName
                    }),
                }
            };

            return queryObj;
        }
    }
}
