using NEMILTEC.MVC.Code.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models
{
    public interface IContainerModel : IModel
    {
        ContainerType ContainerType { get; set; }

        long MaxItems { get; set; }

  
        IModel Model { get; set; }


    }

}
