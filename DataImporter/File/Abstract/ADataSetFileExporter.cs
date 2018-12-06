using System.IO;
using NEMILTEC.Interfaces.Service.Data.File;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.File.Abstract
{
    /// <summary>
    /// exports data from DataSet into a file
    /// 
    /// author: chris brown
    /// date created: 24/06/2015
    /// </summary>
    public abstract class ADataTableFileExporter : IFileExporter<DataTable>
    {
        public abstract Stream Export(DataTable item);
    }
}
