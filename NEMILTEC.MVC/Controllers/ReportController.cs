using NEMILTEC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Compilation;
using System.Web.ModelBinding;
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
using NEMILTEC.Service.Data.Database;
using NEMILTEC.Service.Data.Objects.Queries;
using NEMILTEC.Shared.Classes.Serializers;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.MVC.Models.Container.Query;
using NEMILTEC.MVC.Models.Container.Report;
using NEMILTEC.MVC.Models.Properties;
using NEMILTEC.MVC.Models.Report;
using NEMILTEC.Service.Data.File.Importers;
using NEMILTEC.Service.Reporting.Concrete;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Shared.Classes.Data;
using DataTable = NEMILTEC.Shared.Classes.Data.DataTable;
using DataType = NEMILTEC.Shared.Enums.Data.DataType;

namespace NEMILTEC.MVC.Controllers
{
    public class ReportController : Controller
    {

        private IDatabase<DataTable> _db;

        private delegate IEnumerable<IModel> UpdateAllModelDelegate(IEnumerable<IModel> models);
        private delegate IModel UpdateModelDelegate(IModel model);
        private delegate IContainerModel UpdateContainerModelDelegate(IContainerModel model);
        private delegate IEnumerable<IModel> UpdateModelChildrenDelegate(IModel parent, IEnumerable<IModel> children);

        private Dictionary<ModelType, UpdateModelDelegate> _updateFuncMap;
        private Dictionary<ModelType, UpdateAllModelDelegate> _updateAllFuncMap;
        private Dictionary<ModelType, UpdateContainerModelDelegate> _updateContainerFuncMap;

        //private Dictionary<ModelType, UpdateModelChildrenDelegate> _updateChildrenFuncMap;

        private Dictionary<ModelType, List<KeyValuePair<ModelType, object>>> _navProps;

        public ReportController()
        {
            _updateAllFuncMap = new Dictionary<ModelType, UpdateAllModelDelegate>()
            {
                {ModelType.Report, new UpdateAllModelDelegate(_UpdateReports)},
                {ModelType.ReportElement, new UpdateAllModelDelegate(_UpdateReportElements)},
                {ModelType.ReportElementParameter, new UpdateAllModelDelegate(_UpdateReportElementParameters)}
            };

            _updateFuncMap = new Dictionary<ModelType, UpdateModelDelegate>()
            {
                {ModelType.Report, new UpdateModelDelegate(_UpdateReport)},
                {ModelType.ReportElement, new UpdateModelDelegate(_UpdateReportElement)},
                {ModelType.ReportElementParameter, new UpdateModelDelegate(_UpdateReportElementParameter)},
            };

            _updateContainerFuncMap = new Dictionary<ModelType, UpdateContainerModelDelegate>()
            {
                {ModelType.Report, new UpdateContainerModelDelegate(_UpdateReportContainer)},
                 {ModelType.ReportElement, new UpdateContainerModelDelegate(_UpdateReportElementContainer)},
                 {ModelType.ReportElementParameter, new UpdateContainerModelDelegate(_UpdateReportElementParameterContainer)},
            };

            //_updateChildrenFuncMap = new Dictionary<ModelType, UpdateModelChildrenDelegate>()
            //{
            //     {ModelType.Report, (model, children) => children},
            //     {ModelType.ReportElement, new UpdateModelChildrenDelegate(_UpdateReportElementChildren)},
            //     {ModelType.ReportElementParameter, (model, children) => children},
            //};


            _navProps = new Dictionary<ModelType, List<KeyValuePair<ModelType, object>>>()
            {
                {
                    ModelType.Report, new List<KeyValuePair<ModelType, object>>()
                    {
                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.ReportElement,
                        _CreateExpression<Domain.Report>(q =>  q.Elements)),

                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.ReportOutputType,
                        _CreateExpression<Domain.Report>(q =>  q.OutputType)),

                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.Document,
                        _CreateExpression<Domain.Report>(q =>  q.TemplateFile))
                    }
               },
                { 
                    ModelType.ReportElement, new List<KeyValuePair<ModelType, object>>()
                    {
                        //new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.ReportElementParameter,
                        //_CreateExpression<Domain.ReportElement>(q =>  q.Parameters)),

                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.Report,
                        _CreateExpression<Domain.ReportElement>(q =>  q.Report)),

                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.Query,
                        _CreateExpression<Domain.ReportElement>(q =>  q.Query)),

                        new System.Collections.Generic.KeyValuePair<ModelType, object>(ModelType.ReportElementType,
                        _CreateExpression<Domain.ReportElement>(q =>  q.ReportElementType))
                    }
                }
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
            var vmContainer = dataSvc.CreateContainerModel((int)ModelType.Report);

            //TODO should go in JSON template + loaded from file
            var tableContainer = new TableContainerModel(vmContainer);

            _UpdateContainer(tableContainer);

            var vmPage = new PageModel()
            {
                //Test = new DataEntity() {
                //    Properties = new Dictionary<string, DataProperty>()
                //    {
                //        { "Test1", new DataProperty() { Name = "Test1", Type=DataType.Number, Value=2222} }
                //    }
                //},
                Title = "Reports",
                CategoryName = "Report",
                Children = new List<IModel>() { tableContainer }
            };

            return View(vmPage);
        }

        //[HttpPost]
        //public JsonResult Execute(long queryId)
        //{
        //    var context = new DataContext(false);
        //    var dataSvc = new DataAccess(context);
        //    var queryModel = dataSvc.Get((int)ModelType.Query, queryId);

        //    ISQLObject sqlObj = _CreateQuery((QueryModel)queryModel);

        //    string sql = sqlObj.ToSQL();

        //    var dt = _db.ExecuteQuery(sql);

        //    //byte[] data = BinarySerializer.Serialize(dt);

        //    //var output = new QueryOutput()
        //    //{
        //    //    QueryId = queryId,
        //    //    Data = data,
        //    //    TotalRows = dt.Rows.Count(),
        //    //    ResultType = (int)QueryResultType.Success
        //    //};

        //    //var queryOutputRepos = new EFDataRepository<Domain.QueryOutput>(context);

        //    //queryOutputRepos.AddOrUpdate(output);


        //    var vm = new TableContainerModel()
        //    {
        //        ShowFilters = false,
        //        IsEditable = false,
        //        Title = "Results"
        //    };

        //    vm.Children = dt.Rows.Select(
        //            r => new Model()
        //            {
        //                Properties = r.Values.Select((v, i) =>
        //                    new ModelProperty()
        //                    {
        //                        Type = DataType.String,
        //                        Value = v,
        //                        Name = dt.Columns[i]
        //                    }
        //                ).ToDictionary(p => p.Name, p => p)
        //            }
        //        ).Cast<IModel>().ToList();

        //    vm.Properties = vm.Properties.Append(
        //        dt.Columns.Select(c => new ModelProperty() { Name = c, Type = DataType.String })
        //        .ToDictionary(c => c.Name, c => c).ToArray()
        //    );

        //    System.Threading.Thread.Sleep(2000);

        //    return Json(vm, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult Output(long queryId)
        //{
        //    var context = new DataContext(false);
        //    var queryOutputRepos = new EFDataRepository<Domain.QueryOutput>(context);
        //    var output = queryOutputRepos.Query(o => o.QueryId == queryId).Last();
        //    var dt = BinarySerializer.Deserialize<DataTable>(output.Data);

        //    return View(new QueryOutputModel() { Data = dt });
        //}

  

        #region data
        [HttpPost]
        public JsonResult GetChildren(int type, int? childType, long parentId)
        {
            var context = new DataContext(true);

            ////var reposProps = new DataRepositoryProperties()
            //{
            //    NavigationProperties = (childType.HasValue ? _GetNavigationProperties((ModelType)type, new[] { (ModelType)childType }) : _GetNavigationProperties((ModelType)type))
            //};

            var dataSvc = new DataAccess(context);

            var parentModel = dataSvc.Get(type, parentId);
            parentModel.Children = new List<IModel>();

            var children = dataSvc.GetChildren(type, parentId);

            var vm = new TabContainerModel(children.Select(child => (IModel)_UpdateContainer(new TableContainerModel((IContainerModel)child))).ToList());

            if ((ModelType)type == ModelType.ReportElement)
            {
                vm.Children.Add(_CreateReportElementData(parentModel));
            }

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
                    Type = j.QueryJoinType != null ? (JoinType)j.QueryJoinType.Id : JoinType.Inner,
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
            var container = _updateContainerFuncMap[model.Type](model);

            if (!container.Children.IsNullOrEmpty())
            {
                var childList = new List<IModel>();
                foreach (var child in container.Children)
                {
                    childList.Add(_updateFuncMap[child.Type](child));
                }

                container.Children = childList;
            }

            return container;
        }

        #region containers
        private IContainerModel _UpdateReportContainer(IModel model)
        {

            var container = (TableContainerModel)model;

            container.Filters = new Dictionary<string, NameValueModel>()
            {
                {"Name", new NameValueModel() {Name = "Name", Value = null}},
                {"Title", new NameValueModel() {Name = "Title", Value = null}},
                {"OutputType", new NameValueModel() {Name = "OutputType", Value = null}},
            };

            container.PropertyLists = new Dictionary<string, IContainerModel>()
            {
                {"TemplateFile", new ListContainerModel(new DocumentContainerModel())},
                {"OutputType", new ListContainerModel(new ReportOutputTypeContainerModel())},
            };

            container.Properties = container.Properties.Append(new Dictionary<string, ModelProperty>()
                {
                    {"TemplateFile", new ListModelProperty() {Name = "TemplateFileId", Title="TemplateFile"}},
                    {"OutputType", new ListModelProperty() {Name = "OutputTypeId", Title="OutputType"}},
                }.ToArray());

                container.Properties["TemplateFileId"].IsVisible = false;
                container.Properties["OutputTypeId"].IsVisible = false;

                return container;

        }

        private IContainerModel _UpdateReportElementParameterContainer(IModel model)
        {

            var container = (TableContainerModel)model;


            container.Filters = new Dictionary<string, NameValueModel>()
                {
                    {"Name", new NameValueModel() {Name = "Name", Value = null}},
                    {"Title", new NameValueModel() {Name = "Title", Value = null}},
                };

                //container.PropertyLists = new Dictionary<string, IContainerModel>()
                //{
                //    {"ElementType", new ListContainerModel(new ReportElementTypeContainerModel())},
                //    {"Query", new ListContainerModel(new QueryContainerModel())},
                //};

                //container.Properties = container.Properties.Append(new Dictionary<string, ModelProperty>()
                //{
                //    {"ElementType", new ListModelProperty() {Name = "ReportElementTypeId", Title="ElementType"}},
                //    {"Query", new ListModelProperty() {Name = "QueryId", Title="Query"}},
                //}.ToArray());

                container.Properties["ExpressionId"].IsVisible = false;
                container.Properties["ReportElementId"].IsVisible = false;

                return container;

        }

        private IContainerModel _UpdateReportElementContainer(IModel model)
        {
            var container = (TableContainerModel)model;

            container.Filters = new Dictionary<string, NameValueModel>()
            {
                {"Name", new NameValueModel() {Name = "Name", Value = null}},
                {"Title", new NameValueModel() {Name = "Title", Value = null}},
                {"ElementType", new NameValueModel() {Name = "ElementType", Value = null}},
            };

            container.PropertyLists = new Dictionary<string, IContainerModel>()
            {
                {"ElementType", new ListContainerModel(new ReportElementTypeContainerModel())},
                {"Query", new ListContainerModel(new QueryContainerModel())},
            };

            container.Properties = container.Properties.Append(new Dictionary<string, ModelProperty>()
                {
                    {"ElementType", new ListModelProperty() {Name = "ReportElementTypeId", Title="ElementType"}},
                    {"Query", new ListModelProperty() {Name = "QueryId", Title="Query"}},
                }.ToArray());

                container.Properties["TemplateInfo"].IsVisible = false;
                container.Properties["ReportId"].IsVisible = false;
                container.Properties["ReportElementTypeId"].IsVisible = false;
                container.Properties["QueryId"].IsVisible = false;

                return container;
        }
        #endregion


        #region report
        private IModel _UpdateReport(IModel model)
        {
                var report = (ReportModel)model;

                report.Properties["TemplateFileId"].IsVisible = false;
                report.Properties["OutputTypeId"].IsVisible = false;

                report.Properties = report.Properties.Append(new Dictionary<string, ModelProperty>()
                {
                    {"TemplateFile", new ListModelProperty() {Name = "TemplateFileId", Title="TemplateFile"}},
                    {"OutputType", new ListModelProperty() {Name = "OutputTypeId", Title="OutputType"}},
                }.ToArray());

                if (report.TemplateFile != null)
                {
                    report.Properties["TemplateFile"].Value = report.TemplateFile.Name;
                }

                if (report.OutputType != null)
                {
                    report.Properties["OutputType"].Value = report.OutputType.Name;
                }

            report.Clear();

            return report;
        }

        private IEnumerable<IModel> _UpdateReports(IEnumerable<IModel> models)
        {
            var queryList = new List<IModel>();
            foreach (var model in models)
            {
                queryList.Add(_UpdateReport(model));
            }

            return queryList;
        }
        #endregion

        #region report elements
        private IModel _CreateReportElementData(IModel model)
        {

            var elem = (ReportElementModel) model;

            if (!elem.IsNew)
            {
                var templateInfo = BinarySerializer.Deserialize<DataEntity>(
                  elem.TemplateInfo);

                var templateInfoData = new DataEntity(templateInfo);

                var templateInfoModel = new SegmentContainerModel(new DataContainerModel()
                {
                    Type = elem.Type,
                    ParentId = elem.Id,
                    Title = "Template Info",
                    DataType = DataType.Entity,
                    IsEditable = false,
                    Data = templateInfoData
                });


                return templateInfoModel;
            }

            return null;
        }

        private IModel _UpdateReportElement(IModel model)
        {

                var elem = (ReportElementModel)model;

                elem.Properties["TemplateInfo"].IsVisible = false;
                elem.Properties["ReportId"].IsVisible = false;
                elem.Properties["QueryId"].IsVisible = false;
                elem.Properties["ReportElementTypeId"].IsVisible = false;

                elem.Properties = elem.Properties.Append(new Dictionary<string, ModelProperty>()
                {
                    {"ElementType", new ListModelProperty() {Name = "ReportElementTypeId", Title="ElementType"}},
                    {"Query", new ListModelProperty() {Name = "QueryId", Title="Query"}},
                }.ToArray());

                if (elem.ReportElementType != null)
                {
                    elem.Properties["ElementType"].Value = elem.ReportElementType.Name;
                }

                if (elem.Query != null)
                {
                    elem.Properties["Query"].Value = elem.Query.Name;
                }

            elem.Clear();

            return elem;
        }

        private IEnumerable<IModel> _UpdateReportElements(IEnumerable<IModel> models)
        {
            var projList = new List<IModel>();
            foreach (var model in models)
            {
                projList.Add(_UpdateReportElement(model));
            }

            return projList;
        }
        #endregion

        #region report element params
        private IModel _UpdateReportElementParameter(IModel model)
        {
                var param = (ReportElementParameterModel)model;

                param.Properties["ExpressionId"].IsVisible = false;
                param.Properties["ReportElementId"].IsVisible = false;

                return param;
        }

        private IEnumerable<IModel> _UpdateReportElementParameters(IEnumerable<IModel> models)
        {
            var projList = new List<IModel>();
            foreach (var model in models)
            {
                projList.Add(_UpdateReportElementParameter(model));
            }

            return projList;
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