namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReportElementParameter")]
    public partial class ReportElementParameter : ATrackableEntity
    {
        [Required()]
        [ForeignKey("Element")]
        public long ReportElementId { get; set; }


        public virtual ReportElement Element { get; set; }

        [ForeignKey("Expression")]
        public long? ExpressionId { get; set; }

        public virtual Expression Expression { get; set; }

        //public byte[] Data { get; set; }

        //[ForeignKey("DataType")]
        //public long? DataTypeId { get; set; }

        //public DataType DataType { get; set; }


    }
}
