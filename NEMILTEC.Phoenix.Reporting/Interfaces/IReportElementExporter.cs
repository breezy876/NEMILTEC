
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Reporting.Concrete.Elements;

namespace NEMILTEC.Service.Reporting.Interfaces
{
    /// <summary>
    /// report element exporter
    /// patterns - visitor
    /// </summary>
    public interface IReportElementExporter
    {
        bool Export(TableReportElement item, IReportDocument document);

        bool Export(ChartReportElement item, IReportDocument document);

        bool Export(TextReportElement item, IReportDocument document);
    }
}
