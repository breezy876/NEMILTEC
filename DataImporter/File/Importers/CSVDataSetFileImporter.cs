using System.IO;
using NEMILTEC.Service.Data.File.Abstract;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.File.Importers
{
    public class CSVDataTableFileImporter : ADataTableFileImporter
    {
        private char _colSeperator;

        public CSVDataTableFileImporter(char colSeperator = ',')
        {
            _colSeperator = colSeperator;
        }

        public override bool Import(DataTable dataTable, Stream stream)
        {

            //try
            //{
            //    var lines = FileHelpers.ReadAllLines(fileName);

            //    var columnNames = lines[0].Split(_colSeperator);
            //    var columns = columnNames.Select(c => new DataColumn() { Name = c });

            //    var rows = lines.Skip(1)
            //        .Select(
            //                r => new DataRow() { 
            //                        Cells = r.Split(_colSeperator)
            //                            .Select(c => new DataCell() { Value = c }) 
            //                }
            //        );

            //    item.Rows = rows;
            //    item.Columns = columns;
            //}
            //catch { return false; }
            //return true;
            return false; 

        }

    }
}
