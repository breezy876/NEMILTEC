using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.Service.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.Service.Shared
{
    public static class Document
    {
        public static Stream GetStream(long docId, IDataContext context)
        {
            IDataRepository<Domain.Document> docRepos = new EFDataRepository<Domain.Document>(context);
            var document = docRepos.Get(docId);
            var stream = new MemoryStream(document.Data);
            stream.Position = 0;
            return stream;
        }
    }
}
