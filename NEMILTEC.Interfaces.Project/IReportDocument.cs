using System.IO;

namespace NEMILTEC.Interfaces.Service.Reporting
{
    public interface IReportDocument
    {
        bool Load(Stream stream);
        Stream Save();
    }
}
