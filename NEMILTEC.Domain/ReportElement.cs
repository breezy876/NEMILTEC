namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReportElement")]
    public partial class ReportElement : ATrackableEntity
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
    "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReportElement()
        {
            Parameters = new HashSet<ReportElementParameter>();
        }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public byte[] Output { get; set; }
        //public byte[] Data { get; set; }
        public byte[] TemplateInfo { get; set; }

        [Required()]
        [ForeignKey("Report")]
        public long ReportId { get; set; }


        public virtual Report Report { get; set; }


        [ForeignKey("Query")]
        public long QueryId { get; set; }


        public virtual Query Query { get; set; }


        //[ForeignKey("Expression")]
        //public long ExpressionId { get; set; }

        //public virtual Expression Expression { get; set; }

        [Required()]
        [ForeignKey("ReportElementType")]
        public long ReportElementTypeId { get; set; }


        public virtual ReportElementType ReportElementType { get; set; }


        //[ForeignKey("DataType")]
        //public long? DataTypeId { get; set; }

        //public DataType DataType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
    "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportElementParameter> Parameters { get; set; }


    }
}
