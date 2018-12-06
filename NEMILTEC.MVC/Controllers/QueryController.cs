using NEMILTEC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Data.Database;
using NEMILTEC.Interfaces.Service.Data.File;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.MVC.Code;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.Service.Data;
using NEMILTEC.Service.Data.Database;
using NEMILTEC.Service.Data.Objects.Queries;
using NEMILTEC.Shared.Classes.Serializers;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.MVC.Models.Properties;
using NEMILTEC.Service.Data.File.Importers;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Shared.Classes.Data;
using DataType = NEMILTEC.Shared.Enums.Data.DataType;

namespace NEMILTEC.MVC.Controllers
{
    public class QueryController : Controller
    {

        private IDatabase<DataTable> _db;

        private delegate IEnumerable<IModel> UpdateAllModelDelegate(IEnumerable<IModel> models);
        private delegate IModel UpdateModelDelegate(IModel model);
        private delegate IModel UpdateContainerModelDelegate(IModel IContainerModel);

        private Dictionary<ModelType, UpdateModelDelegate> _updateFuncMap;
        private Dictionary<ModelType, UpdateAllModelDelegate> _updateAllFuncMap;
        private Dictionary<ModelType, UpdateContainerModelDelegate> _updateContainerFuncMap;

        private Dictionary<ModelType, List<KeyValuePair<ModelType, object>>> _navProps;

        public QueryController()
        {
            _updateAllFuncMap = new Dictionary<ModelType, UpdateAllModelDelegate>()
            {
                {ModelType.Query, new UpdateAllModelDelegate(_UpdateQueries)},
                {ModelType.QueryProjection, new UpdateAllModelDelegate(_UpdateProjections)},
                {ModelType.QueryJoin, new UpdateAllModelDelegate(_UpdateJoins)},
                {ModelType.Category, new UpdateAllModelDelegate((models) => { return models; })},
                {ModelType.Project, new UpdateAllModelDelegate((models) => { return models; })}
            };

            _updateFuncMap = new Dictionary<ModelType, UpdateModelDelegate>()
            {
                {ModelType.Query, new UpdateModelDelegate(_UpdateQuery)},
                {ModelType.QueryProjection, new UpdateModelDelegate(_UpdateProjection)},
                {ModelType.QueryJoin, new UpdateModelDelegate(_UpdateJoin)},
                {ModelType.Category, new UpdateModelDelegate((model) => { return model; })},
                {ModelType.Project, new UpdateModelDelegate((model) => { return model; })}
            };

            _updateContainerFuncMap = new Dictionary<ModelType, UpdateContainerModelDelegate>()
            {
                {ModelType.Query, new UpdateContainerModelDelegate(_UpdateQueryContainer)},
                {ModelType.QueryProjection, new UpdateContainerModelDelegate(_UpdateProjectionContainer)},
                {ModelType.QueryJoin, new UpdateContainerModelDelegate(_UpdateJoinContainer)},
            };

            _navProps = new Dictionary<ModelType, List<KeyValuePair<ModelType, object>>>()
            {
                {
                    ModelType.Query, new List<KeyValuePair<ModelType, object>>()
                    {
                        {
                            new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.Project,
                                _CreateExpression<Domain.Query>(q => q.Project))
                        },
                                                                        {
                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.Category,
                                _CreateExpression<Domain.Query>(q => q.Category))
                        },

                        {
                            new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.QueryProjection,
                                _CreateExpression<Domain.Query>(q => q.QueryProjections))
                        },

                        {
                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.QueryJoin,
                                _CreateExpression<Domain.Query>(q =>  q.QueryJoins))
                        }
                    }
                },
                {ModelType.QueryProjection, null },
                {ModelType.QueryJoin, null },
            };

            _db = new Database()
            {
                ConnectionString = Settings.ConnectionString
            };
        }


        //GET: Project
        #region actions
        public ActionResult Index()
        {

            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);
            var vmContainer = dataSvc.CreateContainerModel((int)ModelType.Query);

            //TODO should go in JSON template + loaded from file
            var tableContainer = new TableContainerModel(vmContainer);

            _UpdateContainer(tableContainer);

            var vmPage = new PageModel()
            {
                Title = "Queries",
                CategoryName = "Query",
                Children = new List<IModel>() { tableContainer }
            };

            return View(vmPage);
        }

        [HttpPost]
        public JsonResult Execute(long queryId)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);
            var queryModel = dataSvc.Get((int)ModelType.Query, queryId);

            ISQLObject sqlObj = _CreateQuery((QueryModel)queryModel);

            string sql = sqlObj.ToSQL();

            var dt = _db.ExecuteQuery(sql);

            //byte[] data = BinarySerializer.Serialize(dt);

            //var output = new QueryOutput()
            //{
            //    QueryId = queryId,
            //    Data = data,
            //    TotalRows = dt.Rows.Count(),
            //    ResultType = (int)QueryResultType.Success
            //};

            //var queryOutputRepos = new EFDataRepository<Domain.QueryOutput>(context);

            //queryOutputRepos.AddOrUpdate(output);


            var vm = new TableContainerModel()
            {
                ShowFilters = false,
                IsEditable = false,
                Title = "Results"
            };

            vm.Children = dt.Rows.Select(
                    r => new Model()
                    {
                        Properties = r.Values.Select((v, i) => 
                            new ModelProperty()
                            {
                                Type = DataType.String,
                                Value = v,
                                Name = dt.Columns[i]
                            }
                        ).ToDictionary(p => p.Name, p => p)
                    }
                ).Cast<IModel>().ToList();

            vm.Properties = vm.Properties.Append(
                dt.Columns.Select(c => new ModelProperty() { Name = c, Type = DataType.String })
                .ToDictionary(c => c.Name, c => c).ToArray()
            );

            System.Threading.Thread.Sleep(2000);

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Output(long queryId)
        {
            var context = new DataContext(false);
            var queryOutputRepos = new EFDataRepository<Domain.QueryOutput>(context);
            var output = queryOutputRepos.Query(o => o.QueryId == queryId).Last();
            var dt = BinarySerializer.Deserialize<DataTable>(output.Data);

            return View(new QueryOutputModel() { Data = dt });
        }

        [HttpPost]
        public JsonResult ToSQL(long queryId)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);
            var queryModel = dataSvc.Get((int)ModelType.Query, queryId);

            ISQLObject sqlObj = _CreateQuery((QueryModel)queryModel);
            string sql = sqlObj.ToSQL();

            return Json(sql, JsonRequestBehavior.AllowGet);
        }

        #region data
        [HttpPost]
        public JsonResult GetChildren(int type, int? childType, long parentId)
        {
            var context = new DataContext(false);

            var reposProps = new DataRepositoryProperties()
            {
                NavigationProperties = (childType.HasValue ? _GetNavigationProperties((ModelType)type, new [] {(ModelType)childType}) : _GetNavigationProperties((ModelType)type))
            };

            var dataSvc = new DataAccess(context, reposProps);

            var children = dataSvc.GetChildren(type, parentId);

            var vm = new TabContainerModel(children.Select(child => (IModel)_UpdateContainer(new TableContainerModel((IContainerModel)child))).ToList());

            return Json(vm, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetAll(int type, long? startIndex, long? total, bool addBlank = true)
        {
            var context = new DataContext(false);

            var reposProps = new DataRepositoryProperties()
            {
               NavigationProperties = _GetNavigationProperties((ModelType)type)
            };

            var dataSvc = new DataAccess(context, reposProps);

            var modelCol = dataSvc.GetAll(type, startIndex, total, addBlank);

            if (modelCol.IsNullOrEmpty())
            {
                return Json(null);
            }

            var vmCol = _updateAllFuncMap[(ModelType)type](modelCol);

            //var data = BinarySerializer.Serialize(vmCol);

            return Json(vmCol);
        }

        [HttpGet]
        public JsonResult Get(int type, long id)
        {
            var context = new DataContext(false); ;
            var dataSvc = new DataAccess(context);
            var model = dataSvc.Get(type, id);

            if (model == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var vm = _updateFuncMap[(ModelType)type](model);

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddOrUpdate(int type, string itemJSON)
        {
            var context = new DataContext(false);

            var reposProps = new DataRepositoryProperties()
            {
                NavigationProperties = _GetNavigationProperties((ModelType)type)
            };

            var dataSvc = new DataAccess(context, reposProps);

            var modelCol = dataSvc.AddOrUpdate(type, itemJSON);

            if (modelCol.IsNullOrEmpty())
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var vmCol = _updateAllFuncMap[(ModelType)type](modelCol).ToArray();

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddOrUpdateAll(int type, IEnumerable<string> itemJSONCol)
        {
            var context = new DataContext(false);

            var reposProps = new DataRepositoryProperties()
            {
                NavigationProperties = _GetNavigationProperties((ModelType)type)
            };

            var dataSvc = new DataAccess(context, reposProps);

            var modelCol = dataSvc.AddOrUpdate(type, itemJSONCol);

            if (modelCol.IsNullOrEmpty())
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var vmCol = _updateAllFuncMap[(ModelType)type](modelCol).ToArray();

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remove(int type, string itemJSON)
        {
            var context = new DataContext(false);

            var reposProps = new DataRepositoryProperties()
            {
                NavigationProperties = _GetNavigationProperties((ModelType)type)
            };

            var dataSvc = new DataAccess(context, reposProps);

            var modelCol = dataSvc.Remove(type, itemJSON);

            if (modelCol.IsNullOrEmpty())
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var vmCol = _updateAllFuncMap[(ModelType)type](modelCol).ToArray();

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RemoveAll(int type, IEnumerable<string> itemJSONCol)
        {
            var context = new DataContext(false);

            var reposProps = new DataRepositoryProperties()
            {
                NavigationProperties = _GetNavigationProperties((ModelType)type)
            };

            var dataSvc = new DataAccess(context, reposProps);

            var modelCol = dataSvc.Remove(type, itemJSONCol);

            if (modelCol.IsNullOrEmpty())
            {
                return Json(modelCol, JsonRequestBehavior.AllowGet);
            }

            var vmCol = _updateAllFuncMap[(ModelType)type](modelCol).ToArray();

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UploadFile(int type)
        {
            if (HttpContext.Request.Files.Count > 0)
            {

                var context = new DataContext(false);

                var file = HttpContext.Request.Files[0];
                var fileStream = file.InputStream;

                var dataSvc = new DataAccess(context);

                var items = dataSvc.Import(type, fileStream);

                items = _updateAllFuncMap[(ModelType)type](items).ToArray();

                return Json(items);
            }
            return null;
        }
        #endregion

        #endregion


        #region private methods
        private DataQuery _CreateQuery(QueryModel queryModel)
        {

            var queryObj = new DataQuery()
            {

                Items = new QueryItems()
                {
                    EntityName = queryModel.TableName,

                }
            };

            if (!queryModel.Projections.IsNullOrEmpty())
            {
                queryObj.Items.Projections = queryModel.Projections.Select(p => new DataQueryFieldParameter()
                {
                    Name = p.Name,
                    EntityName = p.TableName,
                    FieldName = p.ColumnName
                });
            }

            if (!queryModel.Joins.IsNullOrEmpty())
            {
                queryObj.Items.Joins = queryModel.Joins.Select(j => new DataQueryJoinParameter()
                {
                    Type = j.QueryJoinType != null ? (JoinType) j.QueryJoinType.Id : JoinType.Inner,
                    ParentColumnName = j.ParentColumnName,
                    ChildColumnName = j.ChildColumnName,
                    ParentTableName = j.ParentTableName,
                    ChildTableName = j.ChildTableName
                });
            }

            return queryObj;
        }

        private IContainerModel _UpdateContainer(IContainerModel model)
        {
            _updateContainerFuncMap[model.Type](model);

            if (!model.Children.IsNullOrEmpty())
            {
                var childList = new List<IModel>();
                foreach (var child in model.Children)
                {
                    childList.Add(_updateFuncMap[child.Type](child));
                }

                model.Children = childList;
            }

            return model;
        }


        #region containers
        private IModel _UpdateQueryContainer(IModel model)
        {
            Func<IModel, IModel> updateFunc = (x) =>
            {
                var container = (TableContainerModel)x;

                container.Filters = new Dictionary<string, NameValueModel>()
                {
                    {"Name", new NameValueModel() {Name = "Name", Value = null}},
                    {"Project", new NameValueModel() {Name = "Project", Value = null}},
                    {"Category", new NameValueModel() {Name = "Category", Value = null}},
                };

                container.PropertyLists = new Dictionary<string, IContainerModel>()
                {
                    {"Category", new ListContainerModel(new CategoryContainerModel())},
                    {"Project", new ListContainerModel(new ProjectContainerModel())},
                };

                container.Properties = container.Properties.Append(new Dictionary<string, ModelProperty>()
                {
                    {"Category", new ListModelProperty() {Name = "CategoryId", Title="Category"}},
                    {"Project", new ListModelProperty() {Name = "ProjectId", Title="Project"}},
                }.ToArray());

                container.Properties["CategoryId"].IsVisible = false;
                container.Properties["ProjectId"].IsVisible = false;
                container.Properties["ConditionId"].IsVisible = false;
                container.Properties["AggregateConditionId"].IsVisible = false;
                container.Properties["IntelliFlowId"].IsVisible = false;
                container.Properties["IntelliFlowItemId"].IsVisible = false;

                return container;

            };

            model.Update(updateFunc);

            return model;
        }

        private IModel _UpdateJoinContainer(IModel model)
        {
            Func<IModel, IModel> updateFunc = (x) =>
            {
                var container = (TableContainerModel)x;

                container.Filters = new Dictionary<string, NameValueModel>()
                {
                    { "Name", new NameValueModel() {Name = "Name", Value = null} },
                    { "ParentTableName", new NameValueModel() { Name = "ParentTableName", Value = null} },
                    { "ParentColumnName", new NameValueModel() { Name = "ParentColumnName", Value = null} },
                    { "ChildTableName", new NameValueModel() { Name = "ChildTableName", Value = null} },
                    { "ChildColumnName", new NameValueModel() { Name = "ChildColumnName", Value = null} },
                };

                container.Properties["QueryId"].IsVisible = false;
                container.Properties["QueryJoinTypeId"].IsVisible = false;

                return container;

            };

            model.Update(updateFunc);

            return model;
        }

        private IModel _UpdateProjectionContainer(IModel model)
        {
            Func<IModel, IModel> updateFunc = (x) =>
            {
                var container = (TableContainerModel)x;

                container.Filters = new Dictionary<string, NameValueModel>()
                {
                    { "Name", new NameValueModel() { Name = "Name", Value = null} },
                    { "ColumnName", new NameValueModel() {  Name = "ColumnName", Value = null} },
                    { "TableName", new NameValueModel() {  Name = "TableName", Value = null} },
                };

                container.Properties["QueryId"].IsVisible = false;

                return container;

            };

            model.Update(updateFunc);

            return model;
        }
        #endregion

        #region join
        private IModel _UpdateJoin(IModel model)
        {
            Func<IModel, IModel> updateFunc = (x) =>
            {

                var join = (QueryJoinModel)x;

                join.Properties["QueryId"].IsVisible = false;
                join.Properties["QueryJoinTypeId"].IsVisible = false;

                return join;

            };

            model.Update(updateFunc);

            return model;
        }

        private IEnumerable<IModel> _UpdateJoins(IEnumerable<IModel> models)
        {
            var joinList = new List<IModel>();
            foreach (var model in models)
            {
                joinList.Add(_UpdateJoin(model));
            }

            return joinList;
        }
        #endregion


        #region projection
        private IModel _UpdateProjection(IModel model)
        {
            Func<IModel, IModel> updateFunc = (x) =>
            {

                var proj = (QueryProjectionModel)x;

                proj.Properties["QueryId"].IsVisible = false;

                return proj;

            };

            model.Update(updateFunc);

            return model;
        }

        private IEnumerable<IModel> _UpdateProjections(IEnumerable<IModel> models)
        {
            var projList = new List<IModel>();
            foreach (var model in models)
            {
                projList.Add(_UpdateProjection(model));
            }

            return projList;
        }
        #endregion

        #region query
        private IEnumerable<IModel> _UpdateQueries(IEnumerable<IModel> models)
        {
            var queryList = new List<IModel>();
            foreach (var model in models)
            {
                queryList.Add(_UpdateQuery(model));
            }

            return queryList;
        }

        private IModel _UpdateQuery(IModel model)
        {
            Func<IModel, IModel> updateFunc = (x) =>
            {

                var query = (QueryModel)x;

                query.Properties["CategoryId"].IsVisible = false;
                query.Properties["ProjectId"].IsVisible = false;
                query.Properties["ConditionId"].IsVisible = false;
                query.Properties["AggregateConditionId"].IsVisible = false;
                query.Properties["IntelliFlowId"].IsVisible = false;
                query.Properties["IntelliFlowItemId"].IsVisible = false;

                query.Properties = query.Properties.Append((new Dictionary<string, ModelProperty>()
            {
                {"Category", new ListModelProperty() {Name = "CategoryId", Title="Category"}},
                {"Project", new ListModelProperty() { Name = "ProjectId", Title="Project"}},
            }).ToArray());

                if (query.Category != null)
                {
                    query.Properties["Category"].Value = query.Category.Name;
                }

                if (query.Project != null)
                {
                    query.Properties["Project"].Value = query.Project.Name;
                }

                return query;

            };

            model.Update(updateFunc);

            return model;
        }
        #endregion

        private IEnumerable<object> _GetNavigationProperties(ModelType parentType, IEnumerable<ModelType> types = null)
        {
            var navProps = _navProps[parentType];

            if (navProps.IsNullOrEmpty())
            {
                return null;
            }

            if (!types.IsNullOrEmpty())
            {
                return _navProps[parentType]
                    .Where(kvp => types.Contains(kvp.Key))
                    .Select(kvp => kvp.Value).ToArray();
            }

            return _navProps[parentType].Select(kvp => kvp.Value).ToList();
        }

        private object _CreateExpression<T>(Expression<Func<T, object>> func)
        {
            return func;
        }
        #endregion
    }
}