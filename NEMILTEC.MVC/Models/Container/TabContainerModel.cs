using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public class TabContainerModel : ContainerModel
    {
        
        public TabContainerModel(List<IModel> childTabs)
        {
            ContainerType = Code.Enums.ContainerType.Tabs;
            Children = childTabs;
        }

        public TabContainerModel() : base()
        {
            ContainerType = Code.Enums.ContainerType.Tabs;
        }
    }
}
