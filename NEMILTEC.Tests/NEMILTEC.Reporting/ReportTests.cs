using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEMILTEC.Interfaces.Service.Reporting;
using NEMILTEC.Service.Data;
using NEMILTEC.Service.Reporting.Concrete;
using NEMILTEC.Service.Reporting.Concrete.Documents;
using NEMILTEC.Service.Shared.Data;

namespace NEMILTEC.Tests.NEMILTEC.Reporting
{


    [TestClass]
    public class ReportTests
    {

        [TestMethod]
        public void Test_Report_Generate()
        {
            string connStr = @"Data Source=CJB\SQLEXPRESS;initial catalog=NEMILTEC;persist security info=True;user id=sa;password=Chr1ssy86;MultipleActiveResultSets=True";

            var reportRepos = new EFDataRepository<Domain.Report>();
            var context = new DataContext();
            var reportStream = ReportGenerator.Generate(1, connStr, context);

            using (FileStream outFileStream = new FileStream("C:/Tests/Report_Output.docx", FileMode.Create))
            {
                reportStream.CopyTo(outFileStream);
            }

        }
    }
}
