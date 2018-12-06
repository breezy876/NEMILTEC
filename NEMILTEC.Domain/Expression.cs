namespace NEMILTEC.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Expression")]
    public partial class Expression : ATrackableEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expression()
        {
            Expressions = new HashSet<Expression>();
            Queries = new HashSet<Query>();
            ReportElements = new HashSet<ReportElement>();
            ReportElementParameters = new HashSet<ReportElementParameter>();
            IntelliFlowItems = new HashSet<IntelliFlowItem>();
        }

        [StringLength(255)]
        public string Description { get; set; }

        public byte[] Value { get; set; }



        [ForeignKey("Category")]
        public long? CategoryId { get; set; }

        public Category Category { get; set; }

        [ForeignKey("DataType")]
        public long? DataTypeId { get; set; }

        public DataType DataType { get; set; }

        [Required()]
        [ForeignKey("ExpressionType")]
        public long ExpressionTypeId { get; set; }


        public ExpressionType ExpressionType { get; set; }

        [ForeignKey("IntelliFlow")]
        public long? IntelliFlowId { get; set; }

        public IntelliFlow IntelliFlow { get; set; }

        [ForeignKey("ParentExpression")]
        public long? ParentExpressionId { get; set; }

        public Expression ParentExpression { get; set; }

        [ForeignKey("ParentItem")]
        public long? ParentItemId { get; set; }

        public IntelliFlowItem ParentItem { get; set; }

        [ForeignKey("Project")]
        public long? ProjectId { get; set; }

        public Project Project { get; set; }


        //[ForeignKey("Query")]
        //public long? QueryId { get; set; }

        //public Project Query { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Expression> Expressions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<IntelliFlowItem> IntelliFlowItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Query> Queries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ReportElement> ReportElements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ReportElementParameter> ReportElementParameters { get; set; }
    }
}
