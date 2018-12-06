namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntelliFlowItem")]
    public partial class IntelliFlowItem : ATrackableEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntelliFlowItem()
        {
            Parameters = new HashSet<Expression>();
            Outputs = new HashSet<IntelliFlowItemOutput>();
            ChildItems = new HashSet<IntelliFlowItem>();
            Queries = new HashSet<Query>();
        }

        [StringLength(255)]
        public string Description { get; set; }

        public byte[] Data { get; set; }


        [ForeignKey("DataType")]
        public long? DataTypeId { get; set; }

        public DataType DataType { get; set; }

        [Required()]
        [ForeignKey("ItemType")]
        public long ItemTypeId { get; set; }


        public IntelliFlowItemType ItemType { get; set; }

        [ForeignKey("ParentItem")]
        public long? ParentItemId { get; set; }

        public virtual IntelliFlowItem ParentItem { get; set; }

        [Required()]
        [ForeignKey("IntelliFlow")]
        public long IntelliFlowId { get; set; }

        public virtual IntelliFlow IntelliFlow { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expression> Parameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntelliFlowItemOutput> Outputs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntelliFlowItem> ChildItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Query> Queries { get; set; }


    }

}
