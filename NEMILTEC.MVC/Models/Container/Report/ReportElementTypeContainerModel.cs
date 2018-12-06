using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Container.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportElementTypeContainerModel : ContainerModel
    {
        public ReportElementTypeContainerModel() : base(new ReportElementTypeModel())
        {
            Type = ModelType.ReportElementType;

            Title = "Report Element Type";
        }


        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportElementTypeContainerModel>(copy);
        }

    }
}
