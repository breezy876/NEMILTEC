using NEMILTEC.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportElementParameterModel : Model, IReportElementChild
    {
        public ReportElementParameterModel()
        {
            Type = Code.Enums.ModelType.ReportElementParameter;

            Title = "Report Element Parameter";

            PropertyMappings = new Dictionary<string, string>()
            {
                {"Element", "ElementId" },
                {"Expression", "ExpressionId" },
            };
        }

        [ProtoMember(1)]
        public long ReportElementId { get; set; }

        [ProtoMember(2)]
        public ReportElementModel Element { get; set; }

        [ProtoMember(3)]
        public long? ExpressionId { get; set; }

        [ProtoMember(4)]
        public ExpressionModel Expression { get; set; }


        public override void Clear()
        {
            base.Clear();

            Element = null;
            Expression = null;

        }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportElementParameterModel>(copy);
        }

    }
}
