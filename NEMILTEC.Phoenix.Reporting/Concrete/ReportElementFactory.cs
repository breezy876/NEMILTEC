
using NEMILTEC.Service.Reporting.Concrete.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Reporting;

namespace NEMILTEC.Service.Reporting.Concrete
{

    public static class ReportElementFactory
    {

        private static IDictionary<NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType, IReportElement> _elements = new Dictionary
            <NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType, IReportElement>()
        {
            {NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType.Chart, new ChartReportElement()},
            {NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType.Table, new TableReportElement()},
            {NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType.Text, new TextReportElement()}
        };

        public static IReportElement Create(long type)
        {
            return _elements[(NEMILTEC.Interfaces.Service.Reporting.Enums.ReportElementType)type];
        }
    }
}
