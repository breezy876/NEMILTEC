using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public class ExpressionContainerModel : ContainerModel
    {

        public ExpressionContainerModel() : base(new ExpressionModel())
        {

            Type = ModelType.Expression;

            Title = "Expressions";
        }

    }
}
