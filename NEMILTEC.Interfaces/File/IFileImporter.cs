using System.IO;

namespace NEMILTEC.Interfaces.Service.Data.File
{
    public interface IFileImporter<T> 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dataSource"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        bool Import(T item, Stream stream);
    }
}
