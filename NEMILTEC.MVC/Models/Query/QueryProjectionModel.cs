using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Query
{
    [Serializable]
    [ProtoContract()]
    public class QueryProjectionModel : QueryChildModel
    {
        public QueryProjectionModel()
        {
            Type = ModelType.QueryProjection;
            Title = "Projection";
            CategoryName = "Query";
        }

        //public QueryModel Query { get; set; }

        [ProtoMember(1)]
        public string TableName { get; set; }
        [ProtoMember(2)]
        public string ColumnName { get; set; }


        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<QueryProjectionModel>(copy);
        }

    }
}
