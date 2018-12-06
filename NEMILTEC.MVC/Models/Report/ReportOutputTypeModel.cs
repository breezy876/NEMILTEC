using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportOutputTypeModel : Model
    {
        public ReportOutputTypeModel()
        {
            Type = ModelType.ReportOutputType;

            Title = "Report Output Type";
        }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportOutputTypeModel>(copy);
        }
    }
}
