namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntelliFlowOutput")]
    public partial class IntelliFlowOutput : ATrackableEntity
    {

        [Required]
        public byte[] Data { get; set; }

        [Required]
        [ForeignKey("DataType")]
        public long DataTypeId { get; set; }

        public DataType DataType { get; set; }

        [Required]
        [ForeignKey("IntelliFlow")]
        public long IntelliFlowId { get; set; }

        public IntelliFlow IntelliFlow { get; set; }

    }
}
