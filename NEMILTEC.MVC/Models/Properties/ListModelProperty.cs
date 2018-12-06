using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Properties
{
    public class ListModelProperty : ModelProperty
    {
        public string Title { get; set; }

        public ListModelProperty() : base()
        {
            Type = Shared.Enums.Data.DataType.List;
        }
    }
}
