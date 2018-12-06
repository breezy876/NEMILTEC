using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Reporting.Interfaces;

namespace NEMILTEC.Service.Reporting.Abstract
{
    public abstract class AReportElementInfoImporter : IReportElementInfoImporter
    {
        public abstract bool Import(Report report);
    }
}
