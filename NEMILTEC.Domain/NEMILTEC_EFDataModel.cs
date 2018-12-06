namespace NEMILTEC.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NEMILTEC_EFDataModel : DbContext
    {
        public NEMILTEC_EFDataModel()
            : base("name=NEMILTEC_EFDataModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Expression> Expressions { get; set; }
        public virtual DbSet<ExpressionType> ExpressionTypes { get; set; }
        public virtual DbSet<FileType> FileTypes { get; set; }
        public virtual DbSet<IntelliFlow> IntelliFlows { get; set; }
        public virtual DbSet<IntelliFlowItem> IntelliFlowItems { get; set; }
        public virtual DbSet<IntelliFlowItemOutput> IntelliFlowItemOutputs { get; set; }
        public virtual DbSet<IntelliFlowItemType> IntelliFlowItemTypes { get; set; }
        public virtual DbSet<IntelliFlowOutput> IntelliFlowOutputs { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Query> Queries { get; set; }
        public virtual DbSet<QueryAggregate> QueryAggregates { get; set; }
        public virtual DbSet<QueryFieldParameter> QueryFieldParameters { get; set; }
        public virtual DbSet<QueryGrouping> QueryGroupings { get; set; }
        public virtual DbSet<QueryJoinType> QueryJoinTypes { get; set; }
        public virtual DbSet<QueryOrdering> QueryOrderings { get; set; }
        public virtual DbSet<QueryOrderType> QueryOrderTypes { get; set; }
        public virtual DbSet<QueryOutput> QueryOutputs { get; set; }
        public virtual DbSet<QueryProjection> QueryProjections { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportElement> ReportElements { get; set; }
        public virtual DbSet<ReportElementType> ReportElementTypes { get; set; }
        public virtual DbSet<ReportOutput> ReportOutputs { get; set; }
        public virtual DbSet<ReportParameter> ReportParameters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Variable> Variables { get; set; }
        public virtual DbSet<QueryJoin> QueryJoins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.Document)
                .HasForeignKey(e => e.TemplateFile);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.ReportOutputs)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.OutputFile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expression>()
                .HasMany(e => e.Expression1)
                .WithOptional(e => e.Expression2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Expression>()
                .HasMany(e => e.Queries)
                .WithOptional(e => e.Expression)
                .HasForeignKey(e => e.AggregateCondition);

            modelBuilder.Entity<Expression>()
                .HasMany(e => e.Queries1)
                .WithOptional(e => e.Expression1)
                .HasForeignKey(e => e.Condition);

            modelBuilder.Entity<FileType>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.FileType1)
                .HasForeignKey(e => e.FileType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IntelliFlowItem>()
                .HasMany(e => e.Expressions)
                .WithOptional(e => e.IntelliFlowItem)
                .HasForeignKey(e => e.ParentItemID);

            modelBuilder.Entity<IntelliFlowItem>()
                .HasMany(e => e.IntelliFlowItem1)
                .WithOptional(e => e.IntelliFlowItem2)
                .HasForeignKey(e => e.ParentItemID);

            modelBuilder.Entity<IntelliFlowItem>()
                .HasMany(e => e.Queries)
                .WithOptional(e => e.IntelliFlowItem)
                .HasForeignKey(e => e.ParentItemID);

            modelBuilder.Entity<Query>()
                .HasMany(e => e.Expressions)
                .WithOptional(e => e.Query)
                .HasForeignKey(e => e.QueryID);

            modelBuilder.Entity<Query>()
                .HasMany(e => e.QueryFieldParameters)
                .WithRequired(e => e.Query)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Query>()
                .HasMany(e => e.QueryGroupings)
                .WithRequired(e => e.Query)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Query>()
                .HasMany(e => e.QueryJoins)
                .WithRequired(e => e.Query)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Query>()
                .HasMany(e => e.QueryOrderings)
                .WithRequired(e => e.Query)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Query>()
                .HasMany(e => e.QueryOutputs)
                .WithRequired(e => e.Query)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Query>()
                .HasMany(e => e.QueryProjections)
                .WithRequired(e => e.Query)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueryFieldParameter>()
                .HasMany(e => e.QueryAggregates)
                .WithRequired(e => e.QueryFieldParameter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueryFieldParameter>()
                .HasMany(e => e.QueryGroupings)
                .WithRequired(e => e.QueryFieldParameter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueryFieldParameter>()
                .HasMany(e => e.QueryJoins)
                .WithRequired(e => e.QueryFieldParameter)
                .HasForeignKey(e => e.Child)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueryFieldParameter>()
                .HasMany(e => e.QueryJoins1)
                .WithRequired(e => e.QueryFieldParameter1)
                .HasForeignKey(e => e.Parent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueryFieldParameter>()
                .HasMany(e => e.QueryOrderings)
                .WithRequired(e => e.QueryFieldParameter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QueryFieldParameter>()
                .HasMany(e => e.QueryProjections)
                .WithRequired(e => e.QueryFieldParameter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Report>()
                .HasMany(e => e.ReportElements)
                .WithRequired(e => e.Report)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Report>()
                .HasMany(e => e.ReportOutputs)
                .WithRequired(e => e.Report)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Report>()
                .HasMany(e => e.ReportParameters)
                .WithRequired(e => e.Report)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReportElementType>()
                .HasMany(e => e.ReportElements)
                .WithRequired(e => e.ReportElementType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.QueryOutputs)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
