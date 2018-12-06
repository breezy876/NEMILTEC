using NEMILTEC.MVC.Models.Container;
using NEMILTEC.MVC.Models.Container.Report;
using NEMILTEC.MVC.Models.Query;
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
    public class ReportElementModel : Model, IReportChild
    {

        public ReportElementModel()
        {
            Parameters = new List<ReportElementParameterModel>();

            Type = Code.Enums.ModelType.ReportElement;

            Title = "Report Element";

            PropertyMappings = new Dictionary<string, string>()
            {
                {"Query", "QueryId" },
                {"ReportElementType", "ReportElementTypeId" },
            };
        }

        //public byte[] Output { get; set; }
        //public byte[] TemplateInfo { get; set; }

        [ProtoMember(1)]
        public byte[] TemplateInfo { get; set; }

        [ProtoMember(2)]
        public long ReportId { get; set; }

        [ProtoMember(3)]
        public long QueryId { get; set; }

        [ProtoMember(4)]
        public QueryModel Query { get; set; }

        [ProtoMember(5)]
        public long ReportElementTypeId { get; set; }

        [ProtoMember(6)]
        public ReportElementTypeModel ReportElementType { get; set; }

        [ProtoMember(7)]
        public List<ReportElementParameterModel> Parameters { get; set; }

        public override void Clear()
        {
            base.Clear();

            Query = null;
            ReportElementType = null;
        }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportElementModel>(copy);
        }

    }
}
