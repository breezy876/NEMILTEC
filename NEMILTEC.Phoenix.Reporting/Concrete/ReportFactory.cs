using System;

using NEMILTEC.Service.Reporting.Abstract;
using NEMILTEC.Service.Reporting.Concrete.Exporters;
using NEMILTEC.Service.Reporting.Interfaces;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Service.Data.Expressions;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.Service.Data.Objects.Queries;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Data;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.Interfaces.Service.Reporting.Enums;
using NEMILTEC.Service.Reporting.Concrete.Documents;
using NEMILTEC.Service.Reporting.Concrete.Elements.TemplateInfo;
using NEMILTEC.Shared.Classes.Serializers;

namespace NEMILTEC.Service.Reporting.Concrete
{
    /// <summary>
    /// creates required report element exporter instance based on report output type
    /// factory pattern
    /// </summary>
    public static class ReportFactory
    {

        private static IDictionary<ReportOutputType, IReportElementExporter> _exporters = new Dictionary
        <ReportOutputType, IReportElementExporter>()
        {
            {ReportOutputType.PowerPoint, new PowerPointReportElementExporter()},
            {ReportOutputType.Word, new WordReportElementExporter()},
            {ReportOutputType.Excel, new ExcelReportElementExporter()}
        };

        private static IDictionary<ReportOutputType, IReportDocument> _documents = new Dictionary
<ReportOutputType, IReportDocument>()
        {
            {ReportOutputType.PowerPoint, new PowerPointPresentation()},
            {ReportOutputType.Word, new WordDocument()},
            {ReportOutputType.Excel, new ExcelWorkbook()}
        };

        #region private methods
        private static NEMILTEC.Interfaces.Service.Reporting.ReportElementParameter _CreateElementParameter(Domain.ReportElementParameter reportElementParam, IDataContext context)
        {
            var newElementParam = new NEMILTEC.Interfaces.Service.Reporting.ReportElementParameter
            {
                Name = reportElementParam.Name,
                Value =
                    BinarySerializer.Deserialize(
                        (NEMILTEC.Shared.Enums.Data.DataType) reportElementParam.Expression.DataType.Id, reportElementParam.Expression.Value),
                DataType = (NEMILTEC.Shared.Enums.Data.DataType) reportElementParam.Expression.DataType.Id
            };


            if (reportElementParam.Expression != null)
            {
                newElementParam.Expression = ExpressionFactory.Create(reportElementParam.Expression, context);
            }

            return newElementParam;
        }

        private static NEMILTEC.Interfaces.Service.Reporting.IReportElement _CreateElement(
            Domain.ReportElement element,
            NEMILTEC.Interfaces.Service.Reporting.Report report,
            IDataContext context)
        {
            var newElement = ReportElementFactory.Create(element.ReportElementType.Id);

            newElement.Name = element.Name;
            newElement.Description = element.Description;
            newElement.Title = element.Title;

            newElement.Report = report;

            if (!element.TemplateInfo.IsNullOrEmpty())
            {
                var templateInfo = ReportElementTemplateInfoSerializer.Deserialize((ReportElementType)element.ReportElementType.Id, element.TemplateInfo);
                newElement.TemplateInfo = templateInfo;
            }

            //if (element.Expression != null)
            //{
            //    newElement.Expression = ExpressionFactory.Create(element.Expression, context);
            //}

            if (element.Query != null)
            {
                newElement.Query = QueryFactory.Create(element.Query);
            }

            if (!element.Parameters.IsNullOrEmpty())
            {
                newElement.Parameters = element.Parameters.Select(p => _CreateElementParameter(p, context)).ToArray();
            }

            return newElement;
        }
        #endregion

        #region public methods
        public static NEMILTEC.Interfaces.Service.Reporting.Report CreateReport(Domain.Report report, IDataContext context)
        {
            var newReport = new NEMILTEC.Interfaces.Service.Reporting.Report
            {
                Name = report.Name,
                Description = report.Description,
                Title = report.Title,
                TemplateFileStream = NEMILTEC.Service.Shared.Document.GetStream(report.TemplateFile.Id, context),
                OutputType = (NEMILTEC.Interfaces.Service.Reporting.Enums.ReportOutputType) report.OutputType.Id
            };

            newReport.Elements = report.Elements.Select(e => _CreateElement(e, newReport, context)).ToArray();

            return newReport;

        }

        public static IReportDocument CreateDocument(ReportOutputType outputType)
        {
            return _documents[outputType];
        }

        public static IReportElementExporter CreateElementExporter(ReportOutputType outputType)
        {
            return _exporters[outputType];
        }
        #endregion
    }
}
