using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QueryProjection")]
    public partial class QueryProjection : ATrackableEntity, IQueryChild
    {
        [Required()]
        [ForeignKey("Query")]
        public long QueryId { get; set; }

        public virtual Query Query { get; set; }

        [Required]
        [StringLength(100)]
        public string TableName { get; set; }

        [Required]
        [StringLength(100)]
        public string ColumnName { get; set; }
    }
}
