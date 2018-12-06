using NEMILTEC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    [ProtoContract()]
    public class ProjectModel : Model
    {

        public ProjectModel()
        {
            Title = "Project";
            Type = ModelType.Project;

            Category = new CategoryModel();
        }

        [ProtoMember(1)]
        public long? CategoryId { get; set; }
        [ProtoMember(2)]
        public string Description { get; set; }
        [ProtoMember(3)]
        public CategoryModel Category { get; set; }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ProjectModel>(copy);
        }

    }
}
