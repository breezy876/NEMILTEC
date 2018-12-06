using System;
using NEMILTEC.Service.Data.File.Abstract;
using NEMILTEC.Shared.Classes.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.IO;

namespace NEMILTEC.Service.Data.File.Importers
{
    public class ExcelDataTableFileImporter : ADataTableFileImporter
    {

        private string _GetCellText(Cell cell)
        {
            var textElem = cell.Elements<InlineString>().First().Elements<Text>().First();
            return textElem.Text;
        }

        public override bool Import(DataTable dataTable, Stream stream)
        {
            using (SpreadsheetDocument spreadsheetDocument =
                SpreadsheetDocument.Open(stream, false))
            {

                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                var rows = sheetData.Elements<Row>();
                var columnsRow = rows.First();

                var columnNames = columnsRow.Elements<Cell>().Select(c => _GetCellText(c)).ToArray();

                var dataTableRows = new List<DataRow>();

                foreach (Row r in rows.Skip(1))
                {
                    dataTableRows.Add(new DataRow() { Values = r.Elements<Cell>().Select(c => _GetCellText(c)).ToArray() });
                }

                dataTable.Columns = columnNames;
                dataTable.Rows = dataTableRows.ToArray();

                return true;
            }
        }
    }
}
