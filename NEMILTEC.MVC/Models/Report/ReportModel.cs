using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Report
{
    [Serializable]
    [ProtoContract]
    public class ReportModel : Model
    {

        public ReportModel()
        {
            Type = ModelType.Report;
            Title = "Report";

            Elements = new List<ReportElementModel>();

            PropertyMappings = new Dictionary<string, string>()
            {
                {"TemplateFile", "TemplateFileId" },
                {"OutputType", "OutputTypeId" },
            };
        }

        [ProtoMember(1)]
        public List<ReportElementModel> Elements { get; set; }

        [ProtoMember(2)]
        public string Description { get; set; }

        [ProtoMember(3)]
        public long OutputTypeId { get; set; }

        [ProtoMember(4)]
        public long? TemplateFileId { get; set; }

        [ProtoMember(5)]
        public DocumentModel TemplateFile { get; set; }

        [ProtoMember(6)]
        public ReportOutputTypeModel OutputType { get; set; }

        public override void Clear()
        {
            base.Clear();

            TemplateFile = null;
            OutputType = null;
        }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ReportModel>(copy);
        }


    }
}
