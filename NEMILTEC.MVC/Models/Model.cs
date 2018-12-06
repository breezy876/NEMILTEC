using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;
using AutoMapper;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Shared.Classes.Serializers;
using NEMILTEC.Shared.Enums.Data;
using ProtoBuf;
using ModelType = NEMILTEC.MVC.Code.Enums.ModelType;

namespace NEMILTEC.MVC.Models
{


    [Serializable]
    [ProtoContract()]
    public class Model : IModel
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        public long Index { get; set; }

        [ProtoMember(3)]
        public ModelType Type { get; set; }

        [ProtoMember(4)]
        public bool IsNew { get; set; }

        [ProtoMember(5)]
        public bool IsVisible { get; set; }

        [ProtoMember(6)]
        public bool IsEditable { get; set; }

        [ProtoMember(7)]
        public bool IsContainer { get; set; }

        [ProtoMember(8)]
        public ModelType ParentType { get; set; }

        [ProtoMember(9)]
        public List<IModel> Children { get; set; }

        [ProtoMember(10)]
        public Dictionary<string, ModelProperty> Properties { get; set; }

        [ProtoMember(11)]
        public Dictionary<string, string> PropertyMappings { get; set; }

        [ProtoMember(12)]
        public string CategoryName { get; set; }

        [ProtoMember(13)]
        public string Name { get; set; }

        [ProtoMember(14)]
        public string Title { get; set; }

        [ProtoMember(15)]
        public List<ModelCommand> Commands { get; set; }

        [ProtoMember(16)]
        public bool IsChild { get; set; }

        public Model()
        {

            IsEditable = true;
            IsVisible = true;

            Children = new List<IModel>();

            PropertyMappings = new Dictionary<string, string>();

            Properties = new Dictionary<string, ModelProperty>()
            {
                {"ID", new ModelProperty() {Name = "ID", IsVisible = true, Type = DataType.Number}}
            };

            Commands = new List<ModelCommand>();

            Title = string.Empty;
        }


        public virtual void Initialize(Func<IModel, IModel> initFunc = null)
        {
            if (initFunc != null)
            {
                initFunc(this);
            }

            Properties["ID"].Value = Id;
        }

        public virtual void Update(Func<IModel, IModel> updateFunc = null)
        {
            if (updateFunc != null)
            {
                updateFunc(this);
            }
        }

        public virtual void Clear()
        {
            //Children = new List<IModel>();
            //Properties = new Dictionary<string, ModelProperty>();
            //PropertyMappings = new Dictionary<string, string>();
        }

        public virtual IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<Model>(copy);
        }


}
}
