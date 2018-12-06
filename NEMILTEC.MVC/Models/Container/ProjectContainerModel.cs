using NEMILTEC.MVC.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public class ProjectContainerModel : ContainerModel
    {
        public ProjectContainerModel() : base(new ProjectModel())
        {
            Type = ModelType.Project;

            Title = "Project";
        }

    }
}
