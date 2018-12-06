using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.MVC.Code
{
    /// <summary>
    /// dynamic view model builder
    /// uses reflection
    /// prepares view models for dynamic rendering with angular
    /// creates child view model hierarchies
    /// </summary>
    /// 
    public static class ModelBuilder
    {

        static ModelBuilder()
        {
            _excludeProps =
            typeof(IModel).GetProperties().Select(p => p.Name).Union(
                typeof(IContainerModel).GetProperties().Select(p => p.Name)).ToList();

            _excludeProps.Remove("Name");

            Initialize();
        }

        private static readonly List<string> _excludeProps;

        private static Dictionary<ModelType, IContainerModel> _containerModelDic;
        private static Dictionary<ModelType, IModel> _modelDic;
        private static Dictionary<ModelType, IDataRepository<IDataEntity>> _dataSrcDic;

        private static Shared.Enums.Data.DataType _GetPropertyDataType(Type propType)
        {
            return NEMILTEC.Shared.Classes.TypeMapper.Convert(propType);
        }

        private static void Initialize()
        {

            _modelDic = new Dictionary<ModelType, IModel>(ViewModelBuilderMappings.ModelMappings);
            _containerModelDic = new Dictionary<ModelType, IContainerModel>(ViewModelBuilderMappings.ContainerMappings);
            _dataSrcDic = new Dictionary<ModelType, IDataRepository<IDataEntity>>(DataMappings.DataSourceMappings);
        }

        public static IModel Build(string name, object obj)
        {
            Type type = obj.GetType();

            var propDic = type.GetProperties()
                .Select(p => new ModelProperty() { Name = p.Name, Type = _GetPropertyDataType(p.PropertyType), Value = p.GetValue(obj) })
                .ToDictionary(p => p.Name, p => p);

            var model = new Model()
            {
                Name = name,
                Properties = propDic
            };

            return model;

        }

        private static IModel _Build(IModel model)
        {

            Type type = model.GetType();
            PropertyInfo[] properties = type.GetProperties().Where(p => !_excludeProps.Contains(p.Name)).ToArray();

            foreach (PropertyInfo property in properties.Where(p => !_excludeProps.Contains(p.Name)))
            {

                var propType = property.PropertyType;

                if (typeof(IEnumerable<IModel>).IsAssignableFrom(propType)) //property is view model child collection
                {
                    var propModels = ((IList)property.GetValue(model)).Cast<IModel>().ToList();
                    var propModel = (IModel)Activator.CreateInstance(property.PropertyType.GetGenericArguments()[0]);
                    var modelType = propModel.Type;

                    var container = _containerModelDic[modelType];

                    if (!propModels.IsNullOrEmpty())
                    {

                        if (!container.Children.IsNullOrEmpty())
                        {
                            container.Children.Clear();
                        }

                        foreach (var pModel in propModels)
                        {
                            var newPropModel = _Build(pModel);
                            container.Children.Add(newPropModel);
                        }
                    }


                    var newContainer = BuildContainer(container);

                    model.Children.Add(newContainer);

                    //property.SetValue(model, null);
                }
                else if (typeof(IModel).IsAssignableFrom(propType))
                {
                    var propObj = property.GetValue(model);

                    if (propObj != null)
                    {
                        _Build((IModel)propObj);
                    }
                }
                else
                {
                    _AddOrUpdateProperty(model.Properties, property.Name, new ModelProperty()
                    {
                        Name = property.Name,
                        Type = _GetPropertyDataType(propType),
                        Value = property.GetValue(model)
                    });
                }
            }

            model.Initialize();

            return model;
        }


        public static IModel Build(IModel model)
        {


            Type type = model.GetType();
            PropertyInfo[] properties = type.GetProperties().Where(p => !_excludeProps.Contains(p.Name)).ToArray();


            foreach (PropertyInfo property in properties)
            {
                var propType = property.PropertyType;

                if (typeof(IEnumerable<IModel>).IsAssignableFrom(propType)) //property is view model child collection
                {
                    var propModels = ((IList)property.GetValue(model)).Cast<IModel>().ToList();
                    var propModel = (IModel)Activator.CreateInstance(property.PropertyType.GetGenericArguments()[0]);
                    var modelType = propModel.Type;

                    var container = (IContainerModel)_containerModelDic[modelType].Copy();

                    if (!propModels.IsNullOrEmpty())
                    {

                        if (!container.Children.IsNullOrEmpty())
                        {
                            container.Children.Clear();
                        }

                        foreach (var pModel in propModels)
                        {
                            var newPropModel = _Build(pModel);
                            container.Children.Add(newPropModel);
                        }
                    }


                    var newContainer = BuildContainer(container);

                    model.Children.Add(newContainer);

                    //property.SetValue(model, null);
                }
                else if (typeof(IModel).IsAssignableFrom(propType))
                {
                    var propObj = property.GetValue(model);

                    if (propObj != null)
                    {
                        _Build((IModel)propObj);
                    }
                }
                else
                {
                    _AddOrUpdateProperty(model.Properties, property.Name, new ModelProperty()
                    {
                        Name = property.Name,
                        Type = _GetPropertyDataType(propType),
                        Value = property.GetValue(model)
                    });
                }
            }

            model.Initialize();

            return model;
        }

        public static IEnumerable<IModel> Build(ModelType type, DataTable dataTable, IDataContext context)
        {

            var modelList = new List<IModel>();

            var model = ModelFactory.Create(type);

            model = _Build(model);

            var keyPropNames = model.PropertyMappings.Values.Select(v => v.ToLower());

            var propNames = dataTable.Columns
                .Select(c => c.ToLower()).ToArray();

            var exProps = new List<string>(_excludeProps).Select(p => p.ToLower()).ToList();
            exProps.Remove("id");

            var itemType = model.GetType();

            var propDic = itemType.GetProperties()
                .Where(p => !exProps.Contains(p.Name.ToLower()) && 
                propNames.Union(keyPropNames).Contains(p.Name.ToLower()))
                .ToDictionary(p => p.Name.ToLower(), p => p);

            var modelPropDic =
                model.Properties.Values
                    .ToDictionary(p => p.Name.ToLower(), p => p);

            var propMapDic = model.PropertyMappings
                .ToDictionary(kvp => kvp.Key.ToLower(), kvp => kvp.Value.ToLower());

            foreach (var row in dataTable.Rows)
            {
                var item = (IModel)model.Copy();

                for (int colIndex = 0; colIndex < propNames.Length; colIndex++)
                {
                    var propName = propNames[colIndex];
                    var prop = propDic[propName];
                    var cellVal = row.Values[colIndex];

                    //check reflection prop 
                    //if prop type is IModel (FK prop)
                    //query prop.Type (ModelType) where Name = prop.Name
                    //set item.Properties[colName] = query result
                    //set prop[fk] = query result.ID

                    if (typeof (IModel).IsAssignableFrom(prop.PropertyType))
                    {
                        var propObj = (IModel) prop.GetValue(model);

                        if (propObj != null)
                        {
                            var dataSrc = _dataSrcDic[propObj.Type];
                            dataSrc.Context = context;

                            var propData = dataSrc.Query(x => x.Name == cellVal).FirstOrDefault();

                            var propModel = new DataAccess().CreateViewModel((int)propObj.Type, propData);

                            if (propData != null)
                            {
                                prop.SetValue(item, propModel);

                                var keyPropName = propMapDic[propName];
                                var keyProp = propDic[keyPropName];

                                keyProp.SetValue(item, propModel.Id);
                            }
                        }
                    }
                    else
                    {
                        var propType = modelPropDic[propName].Type;
                        var value = StringTypeConverter.Convert(propType, cellVal);
                        prop.SetValue(item, value);
                    }

                }

                var newItem = _Build(item);

                modelList.Add(newItem);

            }

            return modelList;
        }

        public static IModel BuildContainer(IContainerModel container)
        {

            Type type = container.Model.GetType();
            PropertyInfo[] properties = type.GetProperties().Where(p => !_excludeProps.Contains(p.Name)).ToArray();


            foreach (PropertyInfo property in properties.Where(p => !_excludeProps.Contains(p.Name)))
            {

                var propType = property.PropertyType;

                if (typeof(IEnumerable<IModel>).IsAssignableFrom(propType))
                {

                }

                else if (typeof(IModel).IsAssignableFrom(propType))
                {

                    //_AddOrUpdateProperty(container.Properties, property.Name, new ModelProperty()
                    //{
                    //    Type = PropertyDataType.List,
                    //    Name = property.Name
                    //});
                }

                else
                {
                    _AddOrUpdateProperty(container.Properties, property.Name, new ModelProperty()
                    {
                        Name = property.Name,
                        Type = _GetPropertyDataType(propType),
                        Value = property.GetValue(container.Model)
                    });
                }
            }

            container.Initialize();

            return container;
        }

        private static void _AddOrUpdateProperty(Dictionary<string, ModelProperty> propDic, string propName, ModelProperty newProp)
        {

            if (propDic.ContainsKey(propName))
            {
                var oldProp = propDic[propName];
                oldProp.Update(newProp);
            }
            else
            {
                propDic.Add(propName, newProp);
            }

        }
    }


}
