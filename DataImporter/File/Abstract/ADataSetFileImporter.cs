using System.IO;
using NEMILTEC.Interfaces.Service.Data.File;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.File.Abstract
{
    /// <summary>
    /// imports data from a file into DataSet ADT
    /// 
    /// author: chris brown
    /// date created: 24/06/2015
    /// </summary>
    public abstract class ADataTableFileImporter : IFileImporter<DataTable>
    {
        public abstract bool Import(DataTable dataTable, Stream stream);
    }
}
