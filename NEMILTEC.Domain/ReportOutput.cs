namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReportOutput")]
    public partial class ReportOutput : ATrackableEntity
    {
        [Required()]
        [ForeignKey("Report")]
        public long ReportId { get; set; }


        public virtual Report Report { get; set; }

        [Required()]
        [ForeignKey("OutputFile")]
        public long OutputFileId { get; set; }

   
        public virtual Document OutputFile { get; set; }


    }
}
