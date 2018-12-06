using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Data.File;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Container.Query;
using NEMILTEC.Service.Data;
using NEMILTEC.Service.Data.File.Importers;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Shared.Classes.Data;
using NEMILTEC.Shared.Classes.Helpers;
using NEMILTEC.Shared.Classes.Serializers;
using NEMILTEC.Shared.Enums.Data;
using ModelType = NEMILTEC.MVC.Code.Enums.ModelType;

namespace NEMILTEC.MVC.Code
{
    /// <summary>
    /// data access service - does database CRUD + builds models for controller/view
    /// TODO - data access logic in service layer/separate class, model creation logic in separate class
    /// </summary>
    public class DataAccess
    {

        private Dictionary<ModelType, IDataRepository<IDataEntity>> _dataSourceMapDic;
        private Dictionary<ModelType, Expression<Func<IDataEntity, object>>> _childSelectorMapDic;
        private Dictionary<ModelType, Dictionary<ModelType, IDataRepository<IDataEntity>>> _ChildReposMapDic;
        private Dictionary<ModelType, Tuple<Type, Type>> _typeMapDic;
        private Dictionary<ModelType, Type> _containerModelDic;
        private Dictionary<ModelType, Action<IModel, long>> _newChildActionDic;
        private Dictionary<ModelType, System.Linq.Expressions.Expression<Func<IDataEntity, long>>> _parentKeyDic;
        private Dictionary<ModelType, Action<IDataEntity, byte[]>> _updateModelDataActionDic;

        private DataRepositoryProperties _reposProps;

        private IDataContext _context;

        public DataAccess()
        {
            _dataSourceMapDic = new Dictionary<ModelType, IDataRepository<IDataEntity>>(DataMappings.DataSourceMappings);
            _typeMapDic = new Dictionary<ModelType, Tuple<Type, Type>>(DataMappings.TypeMappings);
            _ChildReposMapDic = new Dictionary<ModelType, Dictionary<ModelType, IDataRepository<IDataEntity>>>(DataMappings.ChildDataSourceMappings);
            _childSelectorMapDic = new Dictionary<ModelType, Expression<Func<IDataEntity, object>>>(DataMappings.ChildSelectorMappings);
            _containerModelDic = new Dictionary<ModelType, Type>(DataMappings.ContainerMappings);
            _newChildActionDic = new Dictionary<ModelType, Action<IModel, long>>(DataMappings.NewChildActionMappings);
            _parentKeyDic = new Dictionary<ModelType, Expression<Func<IDataEntity, long>>>(DataMappings.ParentKeyMappings);
            _updateModelDataActionDic = new Dictionary<ModelType, Action<IDataEntity, byte[]>>(DataMappings.UpdateModelDataActionMappings);
        }

        public DataAccess(IDataContext context, DataRepositoryProperties props = null) : this()
        {
            _context = context;
            _reposProps = props;
        }

        #region private methods
        private IModel _InitializeViewModel(IModel vm, long index)
        {
            vm.Index = index;
            return vm;
        }

        private IEnumerable<IModel> _AddBlank(int type, List<IModel> models, long? parentId = null)
        {

            var vm = CreateViewModel(type);
            vm.IsNew = true;
            vm.Id = 0;

            if (parentId.HasValue)
            {
                _newChildActionDic[(ModelType) type](vm, parentId.Value);
            }

            models.Insert(0, vm);
            return models
;
        }

        private void _BuildViewModel(IModel vm)
        {
            ModelBuilder.Build(vm);
        }

        private void _BuildViewModels(List<IModel> vmCol)
        {
            foreach (var vmItem in vmCol)
            {
                _BuildViewModel(vmItem);
            }
        }

        private List<IModel> _GetChangedViewModels(IDataEntity model, IModel vm)
        {
            List<IModel> vmCol;

            if (vm.IsChild)
            {
                var parentKey = _parentKeyDic[(ModelType)vm.Type];
                var parentKeyFunc = parentKey.Compile();

                long parentId = (long)parentKeyFunc(model);

                vmCol = GetViewModels((int)vm.Type, (int)vm.ParentType, parentId, null, null).ToList();
            }
            else
            {
                vmCol = GetViewModels((int)vm.Type, null, null).ToList();
            }

            return vmCol;
        }
        #endregion


        #region public methods
        public IEnumerable<IModel> GetViewModels(int type, IEnumerable<string> itemJSONCol)
        {
            var vmList = new List<IModel>();
            foreach (var itemJSON in itemJSONCol)
            {
                var itemVm = GetViewModel(type, itemJSON);
                vmList.Add(itemVm);
            }
            return vmList;
        }

        public IModel GetViewModel(int type, string itemJSON)
        {
            var vmType = _typeMapDic[(ModelType)type].Item2;

            var serializer = new JavaScriptSerializer();
            var obj = JSONSerializer.Deserialize(vmType, itemJSON);

            Model vm = (Model) obj;

            return vm;
        }

        public IDataRepository<IDataEntity> GetDataSource(int type)
        {
            var dataSrc = _dataSourceMapDic[(ModelType)type];
            if (_reposProps != null)
            {
                dataSrc.Properties = _reposProps;
            }
            dataSrc.Context = _context;
            return dataSrc;
        }

        public IEnumerable<IDataEntity> CreateModels(int type, IEnumerable<IModel> vmCol)
        {
            var modelList = new List<IDataEntity>();
            foreach (var vm in vmCol)
            {
                var model = CreateModel(type, vm);
                modelList.Add(model);
            }
            return modelList;
        }

        public IDataEntity CreateModel(int type, IModel vm)
        {

            var modelType = _typeMapDic[(ModelType)type].Item1;
            var vmType = _typeMapDic[(ModelType)type].Item2;

            var model = (IDataEntity)AutoMapper.Mapper.Map(vm, vmType, modelType);

            return model;
        }


        public IModel CreateViewModel(int type, IDataEntity model)
        {
            if (_reposProps != null && _reposProps.IncludeRelated && !_reposProps.NavigationProperties.IsNullOrEmpty())
            {
                DataHelpers.FixNavigationProperties(_reposProps, model);
            }

            var modelType = _typeMapDic[(ModelType)type].Item1;
            var vmType = _typeMapDic[(ModelType)type].Item2;

            var vm = (IModel)AutoMapper.Mapper.Map(model, modelType, vmType);

            return vm;
        }

        public IModel CreateViewModel(int type)
        {
            var vmType = _typeMapDic[(ModelType)type].Item2;
            var vm = (IModel)Activator.CreateInstance(vmType);
            return vm;
        }

        public IEnumerable<IModel> GetViewModels(int type, IEnumerable<IDataEntity> models, bool addBlank, long? parentId = null)
        {

            var result = models.Select(m => CreateViewModel(type, m)).ToList();

            if (addBlank)
            {
                _AddBlank(type, result, parentId);
            }

            return result.Select((m, i) => _InitializeViewModel(m, i)).ToList();

        }

        public IEnumerable<IModel> GetViewModels(int type, int parentType, long parentId, long? startIndex, long? total, bool addBlank = true)
        {
            var dataSrc = GetDataSource(type);

            dataSrc.Properties.LimitTotal = true;

            dataSrc.Properties.StartIndex = startIndex.HasValue ? startIndex.Value : 0;
            dataSrc.Properties.TotalRows = total.HasValue ? total.Value : Common.TotalPerPage;

            var childSelector = _childSelectorMapDic[(ModelType)parentType];
            var exp = childSelector.Compile();

            var models = dataSrc.Query(c => (long)exp(c) == parentId);

            return GetViewModels(type, models, addBlank, parentId);
        }

        public IEnumerable<IModel> GetViewModels(int type, long? startIndex, long? total, bool addBlank = true, long? parentId = null)
        {
            var dataSrc = GetDataSource(type);

            dataSrc.Properties.LimitTotal = true;

            dataSrc.Properties.StartIndex = startIndex.HasValue ? startIndex.Value : 0;
            dataSrc.Properties.TotalRows = total.HasValue ? total.Value : Common.TotalPerPage;

            var models = dataSrc.GetAll().ToList();

            return GetViewModels(type, models, addBlank, parentId);
        }

        public IModel GetViewModel(int type, long id)
        {
            var dataSrc = GetDataSource(type);

            var model = dataSrc.Get(id);
            var vm = CreateViewModel(type, model);
            return vm;
        }



        public IContainerModel CreateContainerModel(int type, bool load = false)
        {

            var vmContainer = ModelFactory.CreateContainer((ModelType)type);

            vmContainer.MaxItems = Common.TotalPerPage;

            ModelBuilder.BuildContainer(vmContainer);

            if (load)
            {
                var vmItems = GetViewModels(type, 0, Common.TotalPerPage).ToList();

                foreach (var vmItem in vmItems)
                {
                    ModelBuilder.Build(vmItem);
                }

                vmContainer.Children = vmItems;

            }

            return vmContainer;
        }


        public IEnumerable<IModel> GetChildren(int parentType, int childType, long parentId)
        {
            var dataSrc = _ChildReposMapDic[(ModelType)parentType][(ModelType)childType];

            var childSelector = _childSelectorMapDic[(ModelType)parentType];
            var exp = childSelector.Compile();

            var childModels = dataSrc.Query(c => (long)exp(c) == parentId);

            var vmCol = GetViewModels(childType, childModels, true).ToList();

            _BuildViewModels(vmCol);

            return vmCol;
        }

        public IEnumerable<IModel> GetChildren(int parentType, long parentId)
        {
            var parentDataSrc = GetDataSource(parentType);

            var parentModel = parentDataSrc.Query(p => ((IDataEntity)p).Id == parentId).FirstOrDefault();
            var parentVm = CreateViewModel(parentType, parentModel);

            _BuildViewModel(parentVm);

            foreach (var childVm in parentVm.Children)
            {
                _AddBlank((int)childVm.Type, childVm.Children, parentId);
                if (!childVm.Children.IsNullOrEmpty())
                {
                    _BuildViewModel(childVm.Children[0]);
                }
            }

            return parentVm.Children;
        }

        public IEnumerable<IModel> GetAll(int type, long? startIndex, long? total, bool addBlank = true)
        {
            var vmCol = GetViewModels(type, startIndex, total, addBlank).ToList();
            _BuildViewModels(vmCol);
            return vmCol;
        }

        public IModel Get(int type, long id)
        {
            var vm = GetViewModel(type, id);
            _BuildViewModel(vm);

            return vm;
        }

        public IEnumerable<IModel> AddOrUpdate(int type, string itemJSON)
        {

            var vm = GetViewModel(type, itemJSON);

            var model = CreateModel(type, vm);
            

            var dataSrc = GetDataSource(type);

            dataSrc.AddOrUpdate(model);

            var vmCol = _GetChangedViewModels(model, vm);

            _BuildViewModels(vmCol);

            return vmCol;
        }

        public IEnumerable<IModel> Remove(int type, string itemJSON)
        {
            var vm = GetViewModel(type, itemJSON);
            var model = CreateModel(type, vm);

            var dataSrc = GetDataSource(type);

            dataSrc.Remove(model.Id);

            var vmCol = _GetChangedViewModels(model, vm);

            _BuildViewModels(vmCol);

            return vmCol;
        }


        public IEnumerable<IModel> AddOrUpdate(int type, IEnumerable<string> itemJSONCol)
        {

            var vmCol = GetViewModels(type, itemJSONCol).ToList();

            var models = CreateModels(type, vmCol).ToList();

            var dataSrc = GetDataSource(type);

            dataSrc.AddOrUpdate(models);

            var newVmCol = _GetChangedViewModels(models[0], vmCol[0]);

            _BuildViewModels(newVmCol);

            return newVmCol;
        }

        public IEnumerable<IModel> Remove(int type, IEnumerable<string> itemJSONCol)
        {
            var vmCol = GetViewModels(type, itemJSONCol).ToList();
            var model = CreateModel(type, vmCol[0]);

            var itemIds = vmCol.Select(i => i.Id).ToList();

            var dataSrc = GetDataSource(type);

            dataSrc.Remove(itemIds);

            var newVmCol = _GetChangedViewModels(model, vmCol[0]);

            _BuildViewModels(newVmCol);

            return newVmCol;
        }

        public IEnumerable<IModel> Import(int type, Stream stream)
        {
            IFileImporter<DataTable> importer = new ExcelDataTableFileImporter();

            var dt = new DataTable();

            importer.Import(dt, stream);

            if (dt != null)
            {
                var items = ModelBuilder.Build((ModelType)type, dt, _context);
                return items;
            }
            return null;
        }

        public bool SaveData(long parentId, int type, string dataJSON, Shared.Enums.Data.DataType dataType)
        {

            var dataObj = JSONSerializer.Deserialize(dataType, dataJSON);
            var dataBytes = BinarySerializer.Serialize(dataType, dataObj);

            var dataSrc = GetDataSource(type);
            var model = dataSrc.Get(parentId);

            _updateModelDataActionDic[(ModelType)type](model, dataBytes);

            dataSrc.AddOrUpdate(model);

            return true;
        }



        #endregion
    }
}