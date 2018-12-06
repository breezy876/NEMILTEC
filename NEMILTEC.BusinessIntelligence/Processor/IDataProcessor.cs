using DataImporter.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Abstract.Processor
{
    /// <summary>
    /// processes data in some way before uploading
    /// runs custom data processes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProcessor<T> : IDataLoader<T>
    {
        void Process();
    }
}
