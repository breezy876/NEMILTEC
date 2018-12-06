using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;
using NEMILTEC.MVC.Code;
using NEMILTEC.Service.Data;
using NEMILTEC.Shared;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Tests;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.Interfaces.Service.Shared.Data;
using System.Data.Entity.Migrations;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.Shared.Classes.Helpers;

namespace NEMILTEC.Tests
{


    [TestClass]
    public class DataTests
    {
        //[TestMethod]
        //public void Test_Context_Seed()
        //{
        //    var context = new DataContext();

        //    //context.Categories.AddRange(new[] {
        //    //    new Domain.Category() { Name = "Category1", IsDeleted = false },
        //    //    new Domain.Category() { Name = "Category2", IsDeleted = false },
        //    //    new Domain.Category() { Name = "Category3", IsDeleted = false },
        //    //    new Domain.Category() { Name = "Category4", IsDeleted = false },
        //    //    new Domain.Category() { Name = "Category5", IsDeleted = false } }
        //    //);

        //    //context.Projects.AddRange(new[] {
        //    //    new Domain.Project() { Name = "Project1", IsDeleted = false },
        //    //    new Domain.Project() { Name = "Project2", IsDeleted = false },
        //    //    new Domain.Project() { Name = "Project3", IsDeleted = false },
        //    //    new Domain.Project() { Name = "Project4", IsDeleted = false },
        //    //    new Domain.Project() { Name = "Project5", IsDeleted = false }
        //    //}
        //    //);

        //    //context.Queries.AddRange(new[] {
        //    //    new Domain.Query() { Name = "Query1", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
        //    //    new Domain.Query() { Name = "Query2", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
        //    //    new Domain.Query() { Name = "Query3", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
        //    //    new Domain.Query() { Name = "Query4", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false },
        //    //    new Domain.Query() { Name = "Query5", TableName = "Project", CategoryId = 1, ProjectId = 1, IsDeleted = false }
        //    //}
        //    //);


        //    context.SaveChanges();
        //}

        [TestMethod]
        public void Test_EFDataRepos_Delete()
        {

            //var dbContext = new DataContext();

            //IDataRepository<IDataEntity> repos = new EFDataRepository<Query>(dbContext) { SaveChanges = true, DeletePermanent = false, TrackChanges = true };


            //repos.Remove(1);
            //repos.Remove(2);
        }

        //[TestMethod]
        //public void Test_EFDataRepos_Add()
        //{

        //    var relEntities = new Expression<Func<IDataEntity, object>>[]
        //    {
        //        (parent) => ((Domain.Query) parent).QueryProjections
        //    };

        //    var dbContext = new DataContext();

        //    IDataRepository userRepos = new EFDataRepository<Domain.User>(dbContext);
        //    var user = (Domain.User) userRepos.Get(1);

        //    IDataRepository qryRepos = new EFDataRepository<Domain.Query>(dbContext)
        //    {
        //        User = user,
        //        IncludeRelated = false,
        //        TrackChanges = true
        //    };

        //    qryRepos.AddOrUpdate(new Domain.Query() {Id = 0, Name = "TestQuery", TableName = "Project"});

        //}

        //[TestMethod]
        //public void Test_EFDataRepos_Update()
        //{


        //    var relEntities = new Expression<Func<IDataEntity, object>>[]
        //    {
        //        (parent) => ((Domain.Query) parent).QueryProjections
        //    };

        //    var dbContext = new DataContext();

        //    IDataRepository userRepos = new EFDataRepository<Domain.User>(dbContext);
        //    var user = (Domain.User)userRepos.Get(1);

        //    IDataRepository qryRepos = new EFDataRepository<Domain.Query>(dbContext)
        //    {
        //        User = user,
        //        IncludeRelated = false,
        //        TrackChanges = true
        //    };

        //    qryRepos.AddOrUpdate(new Domain.Query() { Id = 10002, Name = "TestQuery4", IsDeleted = true});

        //}

        //[TestMethod]
        //public void Test_EFDataRepos_ReadAll()
        //{



        //    var dbContext = new DataContext(false);

        //    IDataRepository qryRepos = new EFDataRepository<Domain.Query>(dbContext)
        //    {
        //        NavigationProperties = new [] { "Category"},
        //        IncludeRelated = true,
        //        TrackChanges = true
        //    };

        //    var queryable = qryRepos.GetAll().AsQueryable().Cast<Domain.Query>();

        //    var qList = queryable.ToList();

        //    qryRepos.AddOrUpdate(new Domain.Query() { Id = 0, Name = "TestQuery", TableName = "Project" });

        //}


        //[TestMethod]
        //public void Test_EFDataRepos_Read()
        //{

        //    var relEntities = new Expression<Func<IDataEntity, object>>[]
        //    {
        //        (parent) => ((Domain.Query) parent).QueryProjections
        //    };

        //    var dbContext = new DataContext();
        //    var queries = dbContext.Queries;
        //    var q = queries.FirstOrDefault();

        //    IDataRepository userRepos = new EFDataRepository<Domain.User>(dbContext);
        //    var user = (Domain.User) userRepos.Get(1);

        //    IDataRepository qryRepos = new EFDataRepository<Domain.Query>(dbContext)
        //    {
        //        User = user,
        //        IncludeRelated = false,
        //        TrackChanges = true
        //    };

        //    var queryable = qryRepos.Get(1);

        //    qryRepos.AddOrUpdate(new Domain.Query() {Id = 0, Name = "TestQuery", TableName = "Project"});

        //}

        //[TestMethod]
        //public void Test_EFDataRepos_Read_2()
        //{



        //    var dbContext = new DataContext();

        //    IDataRepository projRepos = new EFDataRepository<Domain.Project>(dbContext)
        //    {

        //    };

        //    var result = projRepos.GetAll().ToList();


        //}

        [TestMethod]
        public void Test_Context_Read()
        {


        var dbContext = new Service.Shared.Data.DataContext(true);
            //var x = (dbContext.Queries as IQueryable<IDataEntity>).ToList();

            IDataRepository<IDataEntity> readRepos = new EFDataRepository<Query>(dbContext)
            {
                Properties = new DataRepositoryProperties()
                {
                    ExcludeDeleted = true,
                    IncludeRelated = true,
                    NavigationProperties = new List<System.Linq.Expressions.Expression<Func<Domain.Query, object>>>()
                    {
                        {  q => q.QueryProjections }
                    }
                }
            };
            //readRepos.LimitTotal = true;

            //readRepos.StartIndex = 0;
            //readRepos.TotalRows = 5;

            var q1 = readRepos.GetAll().ToList();

            var fixedQ = DataHelpers.FixNavigationProperties(readRepos.Properties, q1[0]);


            var q2 = readRepos.Query(q => (q as Query).CategoryId == 1).ToList();



    }
    }
}
