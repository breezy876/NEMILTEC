using System.Collections.Generic;
using System.IO;
using NEMILTEC.Interfaces.Service.Reporting.Enums;

namespace NEMILTEC.Interfaces.Service.Reporting
{
    public class Report
    {
        /// <summary>
        /// the report name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the report name
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the report description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// the report elements
        /// </summary>
        public IEnumerable<IReportElement> Elements { get; set; }

        /// <summary>
        /// the report output file name
        /// </summary>
        public string OutputFilePath { get; set; }

        /// <summary>
        /// report output type - Excel/Word/PowerPoint/PDF etc
        /// </summary>
        public ReportOutputType OutputType { get; set; }

        /// <summary>
        /// report I/O stream 
        /// </summary>
        public Stream TemplateFileStream { get; set; }

    }
}
