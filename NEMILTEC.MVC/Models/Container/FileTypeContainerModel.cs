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
    public class FileTypeContainerModel : ContainerModel
    {

        public FileTypeContainerModel() : base(new FileTypeModel())
        {

            Type = ModelType.FileType;

            Title = "File Type";
        }

    }
}
