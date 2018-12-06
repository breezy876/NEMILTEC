using NEMILTEC.MVC.Models.Container;
using NEMILTEC.MVC.Models.Container.Report;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;
using DataType = NEMILTEC.Shared.Enums.Data.DataType;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    [ProtoContract]
    public class DataContainerModel : ContainerModel
    {

        public long ParentId { get; set; }
        public object Data { get; set; }
        public DataType DataType { get; set; }

        public DataContainerModel(IModel model) : base(model)
        {
            ContainerType = ContainerType.Data;
        }

        public DataContainerModel()
        {
            ContainerType = ContainerType.Data;
        }

    }
}
