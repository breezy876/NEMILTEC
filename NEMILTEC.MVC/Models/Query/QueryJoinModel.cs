using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.Service.Data.Objects.Queries;
using NEMILTEC.Shared.Classes;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Query
{
    [Serializable]
    [ProtoContract()]
    public class QueryJoinModel : QueryChildModel
    {
        public QueryJoinModel()
        {
            Type = ModelType.QueryJoin;
            Title = "Join";
            CategoryName = "Query";

            PropertyMappings = new Dictionary<string, string>()
            {
                {"QueryJoinType", "QueryJoinTypeId" },
            };
        }
        //public QueryModel Query { get; set; }

        [ProtoMember(1)]
        public string ParentTableName { get; set; }
        [ProtoMember(2)]
        public string ParentColumnName { get; set; }

        [ProtoMember(3)]
        public string ChildTableName { get; set; }
        [ProtoMember(4)]
        public string ChildColumnName { get; set; }

        [ProtoMember(5)]
        public long QueryJoinTypeId { get; set; }

        [ProtoMember(6)]
        public QueryJoinTypeModel QueryJoinType { get; set; }


        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<QueryJoinModel>(copy);
        }

    }
}
