using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.Shared.Classes;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.MVC.Models.Properties;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Query
{
    [Serializable]
    [ProtoContract()]
    public class QueryModel : Model
    {
        public QueryModel()
        {
            Type = ModelType.Query;
            CategoryName = "Query";
            Title = "Query";

            Projections = new List<QueryProjectionModel>();
            Joins = new List<QueryJoinModel>();

            Category = new CategoryModel();
            Project = new ProjectModel();

            PropertyMappings = new Dictionary<string, string>()
            {
                {"Category", "CategoryId" },
                {"Project", "ProjectId" },
            };

        }

        [ProtoMember(1)]
        public long? CategoryId { get; set; }

        [ProtoMember(2)]
        public long? ProjectId { get; set; }

        [ProtoMember(3)]
        public long? ConditionId { get; set; }

        [ProtoMember(4)]
        public long? AggregateConditionId { get; set; }

        [ProtoMember(5)]
        public long? IntelliFlowId { get; set; }

        [ProtoMember(6)]
        public long? IntelliFlowItemId { get; set; }

        [ProtoMember(7)]
        public string TableName { get; set; }

        [ProtoMember(8)]
        public CategoryModel Category { get; set; }
        [ProtoMember(9)]
        public ProjectModel Project { get; set; }

        [ProtoMember(10)]
        public List<QueryProjectionModel> Projections { get; set; }
        [ProtoMember(11)]
        public List<QueryJoinModel> Joins { get; set; }

        public override void Initialize(Func<IModel, IModel> initFunc = null)
        {
            base.Initialize();

           Commands = new List<ModelCommand>()
            {
                new ModelCommand()
                {
                   Id = 1,
                    Url = "/Query/Execute",
                    Data = new {queryId = Id},
                    Title = "Execute",
                    Icon = "fa fa-exclamation"
                }
            };
        }

        public override void Clear()
        {
            base.Clear();

            Category = null;
            Project = null; 

        }

        public override IModel Copy()
        {
         
                var copy = BinarySerializer.Serialize(this);
                return (IModel)BinarySerializer.Deserialize<QueryModel>(copy);
        }

  
    }
}
