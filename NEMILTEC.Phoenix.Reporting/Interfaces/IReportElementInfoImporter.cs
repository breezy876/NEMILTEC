using NEMILTEC.Interfaces.Service.Reporting;

namespace NEMILTEC.Service.Reporting.Interfaces
{
    public interface IReportElementInfoImporter 
    {
        bool Import(Report report);
    }
}
