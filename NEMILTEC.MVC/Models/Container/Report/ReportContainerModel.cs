using System;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models.Container.Query;
using NEMILTEC.MVC.Models.Report;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Container.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportContainerModel : ContainerModel
    {
        public ReportContainerModel() : base(new ReportModel())
        {
            Type = ModelType.Report;

            Title = "Reports";
        }

        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportContainerModel>(copy);
        }

    }
}
