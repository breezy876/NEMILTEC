using NEMILTEC.Service.Reporting.Concrete.Elements;
using NEMILTEC.Service.Reporting.Concrete.Elements.TemplateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Reporting.Enums;

namespace NEMILTEC.Service.Reporting.Concrete
{

    public static class ReportElementTemplateInfoFactory 
    {

        private static IDictionary<ReportElementType, object> _templateInfo = new Dictionary
            <ReportElementType, object>()
        {
            {ReportElementType.Chart, new ChartElementTemplateInfo()},
            {ReportElementType.Table, new TableElementTemplateInfo()},
            {ReportElementType.Text, new TextElementTemplateInfo()}
        };

        public static object Create(long type)
        {
            return _templateInfo[(ReportElementType)type];
        }
    }
}
