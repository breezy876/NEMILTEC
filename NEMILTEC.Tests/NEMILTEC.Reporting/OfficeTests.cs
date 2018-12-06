using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Data;
using NEMILTEC.Service.Reporting.Concrete.Documents;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Tests.NEMILTEC.Reporting
{


    [TestClass]
    public class OfficeTests
    {

        [TestMethod]
        public void Test_WordDocument_ReplaceText()
        {
            IReportDocument doc = new WordDocument();
            var wordDoc = (WordDocument)doc;

            FileStream fileStream = new FileStream("C:/Tests/Document.docx", FileMode.Open);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                CopyStream(fileStream, memoryStream);

                wordDoc.Load(memoryStream);
                wordDoc.ReplaceText("{TEST1}", "AAAAAAAAAAAAAAAAAA");
                wordDoc.ReplaceText("{TEST2}", "BBBBBBBBBBBBBBBBBB");

                var outStream = wordDoc.Save();
                outStream.Position = 0;

                using (FileStream outFileStream = new FileStream("C:/Tests/Document_Output.docx", FileMode.Create))
                {
                    outStream.CopyTo(outFileStream);
                }

            }

        }

        static private void CopyStream(Stream source, Stream destination)
        {
            byte[] buffer = new byte[32768];
            int bytesRead;
            do
            {
                bytesRead = source.Read(buffer, 0, buffer.Length);
                destination.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
        }

        [TestMethod]
        public void Test_WordDocument_InsertRows()
        {

            var dataTable = new DataTable();
            dataTable.Rows = new DataRow[]
            {
                new DataRow() { Values = new [] { "1", "2", "3"} },
                new DataRow() { Values = new [] { "4", "5", "6"} }
            };

            IReportDocument doc = new WordDocument();
            var wordDoc = (WordDocument)doc;

            FileStream fileStream = new FileStream("C:/Tests/Document2.docx", FileMode.Open);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                CopyStream(fileStream, memoryStream);

                wordDoc.Load(memoryStream);
                wordDoc.InsertRows(0, dataTable);

                var outStream = wordDoc.Save();
                outStream.Position = 0;

                using (FileStream outFileStream = new FileStream("C:/Tests/Document2_Output.docx", FileMode.Create))
                {
                    outStream.CopyTo(outFileStream);
                }
            }


        }
    }
}
