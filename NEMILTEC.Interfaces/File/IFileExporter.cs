using System.IO;

namespace NEMILTEC.Interfaces.Service.Data.File
{

    public interface IFileExporter<T> 
    {

        Stream Export(T item);

    }
}
