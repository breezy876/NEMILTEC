using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models;

namespace NEMILTEC.MVC.Code
{
    public class ModelFactory
    {
        public static IModel Create(ModelType type)
        {
            return (IModel)Activator.CreateInstance(DataMappings.ModelMappings[type]);
        }

        public static IContainerModel CreateContainer(ModelType type)
        {
            return (IContainerModel)Activator.CreateInstance(DataMappings.ContainerMappings[type]);
        }
    }
}
