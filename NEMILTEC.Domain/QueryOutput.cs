namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QueryOutput")]
    public partial class QueryOutput : ATrackableEntity
    {

        [Required()]
        [ForeignKey("Query")]
        public long QueryId { get; set; }


        public virtual Query Query { get; set; }


        public short? ResultType { get; set; }

        public long? TotalRows { get; set; }

        [Required()]
        public byte[] Data { get; set; }

    }
}
