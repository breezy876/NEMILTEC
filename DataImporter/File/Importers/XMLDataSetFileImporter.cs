using System;
using System.IO;
using NEMILTEC.Service.Data.File.Abstract;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.File.Importers
{
    public class XMLDataTableFileImporter : ADataTableFileImporter
    {
        public override bool Import(DataTable dataTable, Stream stream)
        {
            throw new NotImplementedException();
        }

    }
}
