using NEMILTEC.MVC.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public class CategoryContainerModel : ContainerModel
    {
        public CategoryContainerModel() : base(new CategoryModel())
        {

            Type = ModelType.Category;

            Title = "Category";

        }

    }
}
