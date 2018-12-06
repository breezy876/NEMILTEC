using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Query")]
    public partial class Query : ATrackableEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Query()
        {
            Expressions = new HashSet<Expression>();
            QueryGroupings = new HashSet<QueryGrouping>();
            QueryJoins = new HashSet<QueryJoin>();
            QueryOrderings = new HashSet<QueryOrdering>();
            QueryOutputs = new HashSet<QueryOutput>();
            QueryProjections = new HashSet<QueryProjection>();
            ReportElements = new HashSet<ReportElement>();
        }


        [Required]
        [StringLength(100)]
        public string TableName { get; set; }


        [ForeignKey("Category")]
        public long? CategoryId { get; set; }

        public Category Category { get; set; }


        [ForeignKey("Project")]
        public long? ProjectId { get; set; }

        public virtual Project Project { get; set; }


        [ForeignKey("Condition")]
        public long? ConditionId { get; set; }

        public virtual Expression Condition { get; set; }

  
        [ForeignKey("AggregateCondition")]
        public long? AggregateConditionId { get; set; }

        public virtual Expression AggregateCondition { get; set; }


        [ForeignKey("IntelliFlow")]
        public long? IntelliFlowId { get; set; }

        public virtual IntelliFlow IntelliFlow { get; set; }


        [ForeignKey("IntelliFlowItem")]
        public long? IntelliFlowItemId { get; set; }

        public virtual IntelliFlowItem IntelliFlowItem { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expression> Expressions { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueryGrouping> QueryGroupings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueryJoin> QueryJoins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueryOrdering> QueryOrderings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueryOutput> QueryOutputs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueryProjection> QueryProjections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportElement> ReportElements { get; set; }
    }
}
