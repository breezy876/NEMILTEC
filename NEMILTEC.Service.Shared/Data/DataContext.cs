using NEMILTEC.Interfaces.Service.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq.Expressions;
using NEMILTEC.Domain.Interfaces;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Service.Shared.Data
{
    public class DataContext : DbContext, IDataContext
    {
        // Your context has been configured to use a 'DataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'NEMILTEC.Domain.DataContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DataContext' 
        // connection string in the application configuration file.
        public DataContext(bool lazyLoadingEnabled)
            : base("ConnectionString")
        {
            //FilteredQueries = new FilteredDbSet<Query>(this, e => (e as IDeletable).IsDeleted);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>("ConnectionString"));
            this.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
        }

        public DataContext()
        : base("ConnectionString")
        {
            //FilteredQueries = new FilteredDbSet<Query>(this, e => ((IDeletable)e).IsDeleted);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>("ConnectionString"));
            this.Configuration.LazyLoadingEnabled = true;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual IDbSet<Domain.Category> Categories { get; set; }
        public virtual IDbSet<Domain.Project> Projects { get; set; }

        public virtual IDbSet<Domain.Query> Queries { get; set; }
        public virtual IDbSet<Domain.QueryProjection> QueryProjections { get; set; }
        public virtual IDbSet<Domain.QueryJoin> QueryJoins { get; set; }
        public virtual IDbSet<Domain.QueryJoinType> QueryJoinTypes { get; set; }

        public virtual IDbSet<NEMILTEC.Domain.Report> Reports { get; set; }
        public virtual IDbSet<NEMILTEC.Domain.ReportOutput> ReportOutputs { get; set; }
        public virtual IDbSet<NEMILTEC.Domain.ReportOutputType> ReportOutputTypes { get; set; }
        public virtual IDbSet<NEMILTEC.Domain.ReportElement> ReportElements { get; set; }
        public virtual IDbSet<NEMILTEC.Domain.ReportElementType> ReportElementTypes { get; set; }
        public virtual IDbSet<NEMILTEC.Domain.ReportElementParameter> ReportElementParameters { get; set; }

        public virtual IDbSet<NEMILTEC.Domain.FileType> FileTypes { get; set; }
        public virtual IDbSet<NEMILTEC.Domain.Document> Documents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Entity<aspnet_UsersInRoles>().HasMany(i => i.Users).WithRequired().WillCascadeOnDelete(false);
        }

        public IDbSet<T> GetSet<T>() where T : class, IDataEntity
        {
            return base.Set<T>() as IDbSet<T>; 
        }


    }


}
