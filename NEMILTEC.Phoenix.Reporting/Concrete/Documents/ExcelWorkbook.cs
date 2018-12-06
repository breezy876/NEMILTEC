using System;
using NEMILTEC.Service.Reporting.Abstract;
using System.IO;

namespace NEMILTEC.Service.Reporting.Concrete.Documents
{

    public class ExcelWorkbook : AReportDocument
    {
        public override bool Load(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override Stream Save()
        {
            throw new NotImplementedException();
        }
    }

}
