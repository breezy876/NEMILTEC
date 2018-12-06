namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Report")]
    public partial class Report : ATrackableEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Report()
        {
            Elements = new HashSet<ReportElement>();
            Outputs = new HashSet<ReportOutput>();
        }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [ForeignKey("TemplateFile")]
        public long? TemplateFileId { get; set; }

        public virtual Document TemplateFile { get; set; }

        [Required()]
        [ForeignKey("OutputType")]
        public long OutputTypeId { get; set; }


        public virtual ReportOutputType OutputType { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportElement> Elements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportOutput> Outputs { get; set; }

    }
}
