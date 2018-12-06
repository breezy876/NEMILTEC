using System.ComponentModel.DataAnnotations.Schema;
using NEMILTEC.Shared.Classes.IO;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.Service.Shared.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;



    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        [ProtoContract()]
        class TableElementTemplateInfo
        {
            [ProtoMember(1)]
            public long Index { get; set; }
            [ProtoMember(2)]
            public string Name { get; set; }
        }

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            context.Categories.AddOrUpdate(
                new Domain.Category() { Name = "Category1", IsDeleted = false },
                new Domain.Category() { Name = "Category2", IsDeleted = false },
                new Domain.Category() { Name = "Category3", IsDeleted = false },
                new Domain.Category() { Name = "Category4", IsDeleted = false },
                new Domain.Category() { Name = "Category5", IsDeleted = false }
            );

            context.Projects.AddOrUpdate(
                new Domain.Project() { Name = "Project1", IsDeleted = false },
                new Domain.Project() { Name = "Project2", IsDeleted = false },
                new Domain.Project() { Name = "Project3", IsDeleted = false },
                new Domain.Project() { Name = "Project4", IsDeleted = false },
                new Domain.Project() { Name = "Project5", IsDeleted = false }
            );

            context.SaveChanges();

            #region queries
            context.Queries.AddOrUpdate(
                new Domain.Query() { Name = "Query1", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
                new Domain.Query() { Name = "Query2", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
                new Domain.Query() { Name = "Query3", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
                new Domain.Query() { Name = "Query4", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
                new Domain.Query() { Name = "Query5", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false }
            );

            context.SaveChanges();

            context.QueryProjections.AddOrUpdate(
                new Domain.QueryProjection() { Name = "ProjectName", TableName = "Project", ColumnName = "Name", QueryId = 1, IsDeleted = false },
                new Domain.QueryProjection() { Name = "ProjectID", TableName = "Project", ColumnName = "ID", QueryId = 1, IsDeleted = false },

                new Domain.QueryProjection() { Name = "ProjectName", TableName = "Project", ColumnName = "Name", QueryId = 2, IsDeleted = false },
                new Domain.QueryProjection() { Name = "ProjectID", TableName = "Project", ColumnName = "ID", QueryId = 2, IsDeleted = false },

                new Domain.QueryProjection() { Name = "ProjectName", TableName = "Project", ColumnName = "Name", QueryId = 3, IsDeleted = false },
                new Domain.QueryProjection() { Name = "ProjectID", TableName = "Project", ColumnName = "ID", QueryId = 3, IsDeleted = false }
            );

            context.QueryJoinTypes.AddOrUpdate(
                new Domain.QueryJoinType() { Name = "Inner" },
                new Domain.QueryJoinType() { Name = "Left Outer"},
                new Domain.QueryJoinType() { Name = "Right Outer" }
            );

            context.SaveChanges();

            context.QueryJoins.AddOrUpdate(
                new Domain.QueryJoin()
                {
                    Name = "ProjectQuery",
                    ChildTableName = "Project",
                    ChildColumnName = "Id",
                    ParentTableName = "Query",
                    ParentColumnName = "ProjectId",
                    QueryId = 1,
                    QueryJoinTypeId = 1,
                    IsDeleted = false
                },
                new Domain.QueryJoin()
                {
                    Name = "CategoryQuery",
                    ChildTableName = "Category",
                    ChildColumnName = "Id",
                    ParentTableName = "Query",
                    ParentColumnName = "CategoryId",
                    QueryId = 1,
                    QueryJoinTypeId = 1,
                    IsDeleted = false
                }
            );

            context.SaveChanges();
            #endregion


            #region report
            context.ReportOutputTypes.AddOrUpdate(
                new Domain.ReportOutputType() { Name = "Word" },
                new Domain.ReportOutputType() { Name = "PowerPoint" }
            );
            context.SaveChanges();

            context.FileTypes.AddOrUpdate(
                new Domain.FileType() { Name = "Word", Extension = ".docx"},
                new Domain.FileType() { Name = "PowerPoint", Extension = ".pptx" }
            );
            context.SaveChanges();

            context.Documents.AddOrUpdate(
                new Domain.Document()
                {
                    Name = "Test Template",
                    Data = FileHelpers.ReadBytes("C:/Tests/ReportTemplate.docx"),
                    FileName = "ReportTemplate.docx",
                    FileTypeId = 1
                }
            );
            context.SaveChanges();

            context.ReportElementTypes.AddOrUpdate(
                new Domain.ReportElementType() {  Name = "Table"},
                new Domain.ReportElementType() { Name = "Chart" },
                new Domain.ReportElementType() { Name = "Text" }
            );
            context.SaveChanges();

            context.Reports.AddOrUpdate(
                new Domain.Report()
                {
                    Name = "Report1",
                    Title = "Test Report 1",
                    OutputTypeId = 1,
                    TemplateFileId = 1,
                    IsDeleted = false
                }
            );
            context.SaveChanges();

            context.ReportElements.AddOrUpdate(
                new Domain.ReportElement()
                {
                    ReportId = 1,
                    Name = "Table1",
                    Title = "Test Table 1",
                    QueryId = 1,
                    ReportElementTypeId = 1,
                    TemplateInfo = BinarySerializer.Serialize(new TableElementTemplateInfo() { Index = 1 }),
                    IsDeleted = false
                },
                new Domain.ReportElement()
                {
                    ReportId = 1,
                    Name = "Table2",
                    Title = "Test Table 2",
                    QueryId = 2,
                    ReportElementTypeId = 1,
                    TemplateInfo = BinarySerializer.Serialize(new TableElementTemplateInfo() { Index = 2  }),
                    IsDeleted = false,
                 
                }
            );
            context.SaveChanges();
            #endregion

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
