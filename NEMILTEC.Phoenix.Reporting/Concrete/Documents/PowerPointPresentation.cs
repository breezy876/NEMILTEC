using System;
using NEMILTEC.Service.Reporting.Abstract;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Reporting.Concrete.Documents
{
    public class PowerPointPresentation : AReportDocument
    {

        public override bool Load(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override Stream Save()
        {
            throw new NotImplementedException();
        }

        public void ReplaceText(string findText, string replaceText)
        {

        }

        public void InsertRows(string tableName, DataTable dataTable)
        {

        }
    }
}
