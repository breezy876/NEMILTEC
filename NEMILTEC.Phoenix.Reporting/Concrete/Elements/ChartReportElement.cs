using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Reporting.Abstract;
using NEMILTEC.Shared.Classes.Data;
using NEMILTEC.Service.Reporting.Concrete.Elements.TemplateInfo;

namespace NEMILTEC.Service.Reporting.Concrete.Elements
{
    public class ChartReportElement : AReportElement
    {
        public ChartReportElement()
        {
   
            Type = NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType.Chart;
        }

        public new ChartElementTemplateInfo TemplateInfo { get; set; }
        public new DataTable Output { get; set; }

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
