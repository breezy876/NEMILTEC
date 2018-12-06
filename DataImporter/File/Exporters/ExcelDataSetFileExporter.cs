using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml;
using NEMILTEC.Service.Data.File.Abstract;
using NEMILTEC.Shared.Classes.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.File.Exporters
{
    public class ExcelDataTableFileExporter : ADataTableFileExporter
    {
        private WorksheetPart _worksheetPart;
        private WorkbookPart _workbookPart;

        private string _GetExcelColumnName(int colNum)
        {
            int dividend = colNum;
            string colName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                colName = Convert.ToChar(65 + modulo).ToString() + colName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return colName;
        }

        private Cell _CreateTextCell(Cell cell, string text)
        {
            cell.DataType = CellValues.InlineString;

            var iStr = new InlineString();
            var t = new DocumentFormat.OpenXml.Spreadsheet.Text { Text = text };
            iStr.AppendChild(t);
            cell.AppendChild(iStr);
            return cell;
        }

        private Cell _InsertCell(SheetData sheet, string columnName, UInt32 rowIndex)
        {
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;

            if (sheet.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheet.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() {RowIndex = rowIndex};
                sheet.Append(row);
            }

     
            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = new Cell() {CellReference = cellReference};
                row.InsertBefore(newCell, refCell);


                return newCell;
            }
        }


        private WorksheetPart _InsertWorksheet(WorkbookPart workbookPart)
        {
            // Add a new worksheet part to the workbook.
            WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());

            string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new sheet.
            uint sheetId = 1;

            string sheetName = "Sheet" + sheetId;

            var sheets = workbookPart.Workbook.
              AppendChild<Sheets>(new Sheets());

            // Append the new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };

            sheets.Append(sheet);

            return newWorksheetPart;
        }

        private WorkbookPart _CreateWorkbook(SpreadsheetDocument document)
        {
            // Add a WorkbookPart to the document.
            WorkbookPart workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();
            return workbookPart;
        }

        public override Stream Export(DataTable dataTable)
        {
            var stream = new MemoryStream();

                using (var document =
                    SpreadsheetDocument.Create(
                        stream,
                        SpreadsheetDocumentType.Workbook))
                {
                    _workbookPart = _CreateWorkbook(document);
                    _worksheetPart = _InsertWorksheet(_workbookPart);

                    SheetData sheet = _worksheetPart.Worksheet.Elements<SheetData>().First();

                    int colIndex = 1;

                    foreach (var col in dataTable.Columns)
                    {
                        string colName = _GetExcelColumnName(colIndex);
                        var cell = _InsertCell(sheet, colName, 1);
                        _CreateTextCell(cell, col);
                        colIndex++;
                    }

                    UInt32 rowIndex = 2;

                    foreach (var row in dataTable.Rows)
                    {
                        colIndex = 1;

                        foreach (var val in row.Values)
                        {
                            string colName = _GetExcelColumnName(colIndex);
                            var cell = _InsertCell(sheet, colName, rowIndex);
                            _CreateTextCell(cell, val);
                            colIndex++;
                        }
                        rowIndex++;
                    }

                    _worksheetPart.Worksheet.Save();
                    _workbookPart.Workbook.Save();

                    return stream;
            }
        }

    }
}
