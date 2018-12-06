using System;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Reporting.Abstract;

namespace NEMILTEC.Service.Reporting.Concrete.Exporters
{
    public class WordReportElementExporter : AReportElementExporter
    {
        public override bool Export(Elements.TableReportElement item, IReportDocument document)
        {
            var wordDoc = (NEMILTEC.Service.Reporting.Concrete.Documents.WordDocument)document;
            wordDoc.InsertRows(item.TemplateInfo.Index, item.Output);
            return true;
        }

        public override bool Export(Elements.ChartReportElement item, IReportDocument document)
        {
            throw new NotImplementedException();
        }

        public override bool Export(Elements.TextReportElement item, IReportDocument document)
        {
            var wordDoc = (NEMILTEC.Service.Reporting.Concrete.Documents.WordDocument)document;
            wordDoc.ReplaceText(item.TemplateInfo.SubstitutionText, item.Output);
            return true;
        }
    }
}
