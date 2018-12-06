namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntelliFlowItemOutput")]
    public partial class IntelliFlowItemOutput : ATrackableEntity
    {

        [Required]
        public byte[] Data { get; set; }

        [Required]
        [ForeignKey("DataType")]
        public long DataTypeId { get; set; }

        public DataType DataType { get; set; }

        [Required]
        [ForeignKey("Item")]
        public long ItemId { get; set; }

        public virtual IntelliFlowItem Item { get; set; }

    }
}
