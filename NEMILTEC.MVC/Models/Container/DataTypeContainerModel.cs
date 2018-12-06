using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public class DataTypeContainerModel : ContainerModel
    {
        public DataTypeContainerModel() : base(new DataTypeModel())
        {
            Type = ModelType.DataType;

            Title = "Data Type";
        }

    }
}
