using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NEMILTEC.Service.Reporting.Abstract;
using NEMILTEC.Shared.Classes.Data;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Reporting.Concrete
{

    /// <summary>
    /// for generating custom reports that have been previously created by user from an existing template
    /// generates reports from a set of user-defined queries and parameters
    /// 
    /// author: chris brown
    /// date created: 24/06/2015
    /// 
    ///
    /// </summary>
    public static class ReportGenerator
    {

        /// <summary>
        /// generates an existing report from a set of known JSON parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Stream Generate(long reportId, string connectionString, IDataContext context)
        {

            IDataRepository<Domain.Report> reportRepos = new EFDataRepository<Domain.Report>(context);

            //fetch report data
            var report = reportRepos.Get(reportId);
            var newReport = ReportFactory.CreateReport(report, context);

            var reportDocument = ReportFactory.CreateDocument((NEMILTEC.Interfaces.Service.Reporting.Enums.ReportOutputType)report.OutputType.Id);

            var reportStream = new MemoryStream();
            newReport.TemplateFileStream.CopyTo(reportStream);

            reportDocument.Load(reportStream);

            foreach (var element in newReport.Elements)
            {
                var elemParams = element.Parameters;
                var elemParamDic = new Dictionary<string, object>();

                if (!elemParams.IsNullOrEmpty())
                {
                    elemParamDic = element.Parameters.ToDictionary(e => e.Name, e => e.Value);
                } 

                element.Import(connectionString, elemParamDic);
                element.Export(reportDocument);
            }

            var outStream = reportDocument.Save();

            outStream.Position = 0;

            return outStream;

        }

    }
}
