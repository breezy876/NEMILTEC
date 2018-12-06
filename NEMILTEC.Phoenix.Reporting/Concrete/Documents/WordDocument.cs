using System;
using NEMILTEC.Service.Reporting.Abstract;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Reporting.Concrete.Documents
{

    public class WordDocument : AReportDocument
    {
        WordprocessingDocument _wordProcessingDoc;
        Document _document;
        MemoryStream _stream;

        public override bool Load(Stream stream)
        {
            _stream = (MemoryStream)stream;
            _wordProcessingDoc = WordprocessingDocument.Open(_stream, true);
            _document = _wordProcessingDoc.MainDocumentPart.Document;
            return true;
        }

        public override Stream Save()
        {
            _document.Save();
            _wordProcessingDoc.Close();
            return _stream;
        }

        public void ReplaceText(string findText, string replaceText)
        {
            var body = _document.Body;

            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Contains(findText))
                {
                    text.Text = text.Text.Replace(findText, replaceText);
                }
            }
        }

        public void InsertRows(long tableIndex, DataTable dataTable)
        {
            var body = _document.Body;
            var tables = body.Descendants<Table>().ToArray();

            var table = tables[tableIndex];

            foreach(var row in dataTable.Rows)
            {
                var tableRow = new TableRow();
                var cells = row.Values.Select(v => new TableCell(new Paragraph(new Run(new Text(v))))).ToArray();
                tableRow.Append(cells);
                table.AppendChild(tableRow);
            }
        }

        public void InsertRows(string tableName, DataTable dataTable)
        {


        }

    }
}
