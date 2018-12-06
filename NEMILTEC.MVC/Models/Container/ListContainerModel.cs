using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    /// <summary>
    /// for dropdown list items
    /// </summary>

    [Serializable]
    public class ListContainerModel : ContainerModelDecorator
    {
        public ListContainerModel(IContainerModel container) : base(container)
        {
            ContainerType = Code.Enums.ContainerType.List;
        }
    }
}
