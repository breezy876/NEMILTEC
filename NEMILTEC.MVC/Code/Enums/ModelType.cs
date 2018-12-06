using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Code.Enums
{
    public enum ModelType
    {
        Query = 1,
        QueryProjection,
        QueryJoin,
        QueryJoinType,
        QueryGrouping,
        QueryOrdering,
        QueryOutput,

        Report,
        ReportElement,
        ReportElementType,
        ReportOutputType,
        ReportElementParameter,
        ReportElementOutput,
        ReportElementTemplateInfo,

        Data,
        Expression,

        Document,
        DataType,
        FileType,

        Category,
        Project
    }
}
