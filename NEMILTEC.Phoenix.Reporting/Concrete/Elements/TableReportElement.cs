using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Reporting.Abstract;
using NEMILTEC.Shared.Classes.Data;
using NEMILTEC.Service.Reporting.Concrete.Elements.TemplateInfo;

namespace NEMILTEC.Service.Reporting.Concrete.Elements
{
    public class TableReportElement : AReportElement
    {

        public TableReportElement()
        {
            Type = NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType.Table;
            TemplateInfo = new TableElementTemplateInfo();
        }

        public new DataTable Output { get; set; }
        public new TableElementTemplateInfo TemplateInfo { get; set; }

        public override bool Export(IReportDocument document)
        {
            _reportElementExporter = ReportFactory.CreateElementExporter(Report.OutputType);
            return _reportElementExporter.Export(this, document);
        }

        public override object Import(string connectionString, IDictionary<string, object> parameters = null)
        {
            Output = (DataTable)ReportElementDataImporter.Import(this, connectionString, parameters);
            return Output;
        }


    }
}
