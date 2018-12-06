using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Interfaces.Service.Reporting.Enums;
using NEMILTEC.Service.Reporting.Concrete;
using NEMILTEC.Service.Reporting.Concrete.Elements;
using NEMILTEC.Service.Reporting.Interfaces;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Reporting.Abstract
{

    /// <summary>
    /// report element
    /// patterns - factory/strategy
    /// </summary>
    /// 
    public abstract class AReportElement : IReportElement
    {
        public Report Report { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public IEnumerable<ReportElementParameter> Parameters { get; set; }

        public virtual object Output { get; set; }
        public virtual object Data { get; set; }
        public virtual object TemplateInfo { get; set; }

        public ISQLObject Query { get; set; }

        public ReportElementType Type { get; set; }

        public IDataExpression Expression { get; set; }

        protected IReportElementExporter _reportElementExporter;

        protected AReportElement()
        {

        }

        public abstract bool Export(IReportDocument document);

        public abstract object Import(string connectionString, IDictionary<string, object> parameters = null);
    }
}
