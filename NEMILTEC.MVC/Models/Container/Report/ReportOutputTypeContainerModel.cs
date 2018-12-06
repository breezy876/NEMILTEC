using System;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models.Report;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Container.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportOutputTypeContainerModel : ContainerModel
    {
        public ReportOutputTypeContainerModel() : base(new ReportOutputTypeModel())
        {
            Type = ModelType.ReportOutputType;

            Title = "Report Output Type";
        }

        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportOutputTypeContainerModel>(copy);
        }

    }
}
