using System.IO;
using NEMILTEC.Interfaces.Service.Reporting;

namespace NEMILTEC.Service.Reporting.Abstract
{
    public abstract class AReportDocument : IReportDocument
    {
        public abstract bool Load(Stream stream);

        public abstract Stream Save();
    }
}
