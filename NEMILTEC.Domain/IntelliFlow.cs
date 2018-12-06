namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntelliFlow")]
    public partial class IntelliFlow : ATrackableEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntelliFlow()
        {
            Expressions = new HashSet<Expression>();
            Items = new HashSet<IntelliFlowItem>();
            ItemOutputs = new HashSet<IntelliFlowItemOutput>();
            Outputs = new HashSet<IntelliFlowOutput>();
            Queries = new HashSet<Query>();
            Variables = new HashSet<Variable>();
        }


        [StringLength(255)]
        public string Description { get; set; }


        [ForeignKey("Category")]
        public long? CategoryId { get; set; }

        public Category Category { get; set; }


        [ForeignKey("Project")]
        public long? ProjectId { get; set; }

        public Project Project { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Expression> Expressions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<IntelliFlowItem> Items { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<IntelliFlowItemOutput> ItemOutputs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<IntelliFlowOutput> Outputs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Query> Queries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Variable> Variables { get; set; }
    }
}
