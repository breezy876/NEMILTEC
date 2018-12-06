using NEMILTEC.MVC.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public abstract class ContainerModelDecorator : ContainerModel
    {

        private IContainerModel _container;

        protected ContainerModelDecorator()
        {
            
        }

        protected ContainerModelDecorator(IContainerModel container) : base(container)
        {
            _container = container;

            MaxItems = container.MaxItems;
            Children = container.Children;
            Properties = container.Properties;
        }
    }
}
