using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Shared.Classes;
using NEMILTEC.MVC.Code.Enums;
using ProtoBuf;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    [ProtoContract]
    public abstract class ContainerModel : Model, IContainerModel
    {
        [ProtoMember(1)]
        public ContainerType ContainerType { get; set; }

        [ProtoMember(2)]
        public long MaxItems { get; set; }

        [ProtoMember(3)]
        public IModel Model { get; set; }

        [ProtoMember(4)]
        public bool IsModal { get; set; }

        protected ContainerModel(IModel model) : base()
        {
            Model = model;

            CategoryName = model.CategoryName;
            Title = model.Title;
            Name = model.Name;
            Type = model.Type;

            IsContainer = true;

        }

        protected ContainerModel() : base()
        {
            IsContainer = true;

        }

    }
}
