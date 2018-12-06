using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Interfaces.Service.Reporting.Enums;

namespace NEMILTEC.Interfaces.Service.Reporting
{

    public interface IReportElement
    {
        Report Report { get; set; }

        string Name { get; set; }
        string Description { get; set; }
        string Title { get; set; }

        IEnumerable<ReportElementParameter> Parameters { get; set; }

        object Output { get; set; }
        object Data { get; set; }
        object TemplateInfo { get; set; }

        IDataExpression Expression { get; set; }
        ISQLObject Query { get; set; }

        ReportElementType Type { get; set; }

        bool Export(IReportDocument document);

        object Import(string connectionString, IDictionary<string, object> parameters = null);
    }
}
