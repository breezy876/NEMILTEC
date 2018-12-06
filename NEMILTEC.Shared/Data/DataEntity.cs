using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using NEMILTEC.Shared.Classes.Serializers;
using NEMILTEC.Shared.Enums.Data;
using ProtoBuf;

namespace NEMILTEC.Shared.Classes.Data
{
    /// <summary>
    /// a dynamic entity
    /// for dynamically creating entity instances
    /// 
    /// author: chris brown
    /// date created: 01/07/2015
    /// </summary>
    /// 
    
    [Serializable]
    [ProtoContract]
    public class DataProperty
    {
        [ProtoMember(1)]
        public bool IsNew { get; set; }

        public DataProperty()
        {
            Type = DataType.String;
        }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public Shared.Enums.Data.DataType Type { get; set; }

        [ProtoMember(4)]
        public object Value { get; set; }
    }

    [Serializable]
    [ProtoContract]
    public class DataEntity
    {
        [ProtoMember(1)]
        public List<DataProperty>  Properties { get; set; }


        [ProtoMember(2)]
        public string Name { get; set; }


        private void _AddBlank()
        {
            var prop = new DataProperty() { IsNew = true};
            Properties.Add(prop);
        }

        public void RemoveBlank()
        {
            Properties.RemoveAt(0);
        }

        public DataEntity()
        {
            Properties = new List<DataProperty>();
        }

        public DataEntity(List<DataProperty> props) : this()
        {
            _AddBlank();
            Properties = Properties.Append(props);
        }


        public DataEntity(object obj) : this()
        {
            Type type = obj.GetType();

            _AddBlank();

            foreach (var prop in type.GetProperties())
            {
                Properties.Add(
                    new DataProperty()
                    {
                        Name = prop.Name,
                        Type = TypeMapper.Convert(prop.PropertyType),
                        Value = prop.GetValue(obj)
                    });
            }

        }
    }
}
