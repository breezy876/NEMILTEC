using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QueryJoin")]
    public partial class QueryJoin : ATrackableEntity, IQueryChild
    {

        [Required()]
        [ForeignKey("Query")]
        public long QueryId { get; set; }


        public virtual Query Query { get; set; }


        [Required]
        [StringLength(100)]
        public string ParentTableName { get; set; }

        [Required]
        [StringLength(100)]
        public string ParentColumnName { get; set; }


        [Required]
        [StringLength(100)]
        public string ChildTableName { get; set; }

        [Required]
        [StringLength(100)]
        public string ChildColumnName { get; set; }


        [Required()]
        [ForeignKey("QueryJoinType")]
        public long QueryJoinTypeId { get; set; }

   
        public virtual QueryJoinType QueryJoinType { get; set; }

    }
}
