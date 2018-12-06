using NEMILTEC.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.Interfaces.Service.Shared.Data;

namespace NEMILTEC.Shared.Classes.Helpers
{
    public static class DataHelpers
    {
        private static IEnumerable<IDataEntity> _GetItems(DataRepositoryProperties props, IEnumerable<IDataEntity> eInput)
        {
            if (props.User != null)
            {
                eInput = eInput.Cast<ITrackable>().Where(q => q.CreatedBy == props.User.Id).Cast<IDataEntity>();
            }

            if (props.ExcludeDeleted)
            {
                eInput = eInput.Cast<IDeletable>().Where(q => !q.IsDeleted).Cast<IDataEntity>();
            }

            return eInput;
        }

        private static IList _CreateList(Type t, IList src)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(t);
            var instance = Activator.CreateInstance(constructedListType);
            return (IList)instance;
        }

        public static IDataEntity FixNavigationProperties(DataRepositoryProperties props, IDataEntity item)
        {
            if (item != null && props != null)
            {


                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (
                    PropertyInfo property in
                        properties.Where(p => typeof (IEnumerable<IDataEntity>).IsAssignableFrom(p.PropertyType))
                            .ToArray())
                {
                    var propType = property.PropertyType;

                    var propCol = (property.GetValue(item) as IEnumerable<IDataEntity>).ToArray();

                    if (!propCol.IsNullOrEmpty())
                    {
                        propCol = _GetItems(props, propCol).ToArray();

                        var containedType = propType.GenericTypeArguments.First();
                        var newList = _CreateList(containedType, propCol);
                        newList.Append(propCol);
                        //var newCol = _GetItem(props, propCol).Select(i =>(typeof(int))i);
                        property.SetValue(item, newList);
                    }
                }

                return item;
            }
            return null;
        }
    }
}
