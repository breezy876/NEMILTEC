using System;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models.Report;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Container.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportElementContainerModel : ContainerModel
    {
        public ReportElementContainerModel() : base(new ReportElementModel())
        {

            Type = ModelType.ReportElement;

            Title = "Report Elements";
        }
        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportElementContainerModel>(copy);
        }

    }
}
