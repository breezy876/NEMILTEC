namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Variable")]
    public partial class Variable : ATrackableEntity
    {

        [ForeignKey("Project")]
        public long? ProjectId { get; set; }

        public virtual Project Project { get; set; }


        [ForeignKey("IntelliFlowItem")]
        public long? IntelliFlowItemId { get; set; }

        public virtual IntelliFlowItem IntelliFlowItem { get; set; }


        [ForeignKey("IntelliFlow")]
        public long? IntelliFlowId { get; set; }

        public virtual IntelliFlow IntelliFlow { get; set; }


        [Required]
        public byte[] Value { get; set; }



    }
}
