
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Reporting.Concrete.Elements;
using NEMILTEC.Service.Reporting.Interfaces;

namespace NEMILTEC.Service.Reporting.Abstract
{
    /// <summary>
    /// report element exporter
    /// patterns - visitor
    /// </summary>
    public abstract class AReportElementExporter : IReportElementExporter
    {
        public abstract bool Export(TableReportElement item, IReportDocument document);

        public abstract bool Export(ChartReportElement item, IReportDocument document);

        public abstract bool Export(TextReportElement item, IReportDocument document);
    }
}
