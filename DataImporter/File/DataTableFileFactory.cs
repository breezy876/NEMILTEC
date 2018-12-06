using NEMILTEC.Interfaces.Service.Data.File;
using NEMILTEC.Service.Data.File.Exporters;
using NEMILTEC.Service.Data.File.Importers;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.File
{
    public static class DataTableFileFactory
    {
        public static IFileImporter<DataTable> CreateImporter(FileType type)
        {
            switch (type)
            {
                case FileType.Csv:
                    return new CSVDataTableFileImporter();
                case FileType.Excel:
                    return new ExcelDataTableFileImporter();
                case FileType.Xml:
                    return new XMLDataTableFileImporter();
            }
            return null;
        }

        public static IFileExporter<DataTable> CreateExporter(FileType type)
        {
            switch (type)
            {
                case FileType.Csv:
                    return new CSVDataTableFileExporter();
                case FileType.Excel:
                    return new ExcelDataTableFileExporter();
                case FileType.Xml:
                    return new XMLDataTableFileExporter();
            }
            return null;
        }
    }
}
