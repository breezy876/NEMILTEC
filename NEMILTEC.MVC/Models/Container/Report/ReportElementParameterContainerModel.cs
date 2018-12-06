using System;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models.Report;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Container.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportElementParameterContainerModel : ContainerModel
    {
        public ReportElementParameterContainerModel() : base(new ReportElementParameterModel())
        {
            Type = ModelType.ReportElementParameter;

            Title = "Parameters";
        }

        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportElementParameterContainerModel>(copy);
        }

    }
}
