using System;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Reporting.Abstract;

namespace NEMILTEC.Service.Reporting.Concrete.Exporters
{
    public class PowerPointReportElementExporter : AReportElementExporter
    {
        public override bool Export(Elements.TableReportElement item, IReportDocument document)
        {
            throw new NotImplementedException();
        }

        public override bool Export(Elements.ChartReportElement item, IReportDocument document)
        {
            throw new NotImplementedException();
        }

        public override bool Export(Elements.TextReportElement item, IReportDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
