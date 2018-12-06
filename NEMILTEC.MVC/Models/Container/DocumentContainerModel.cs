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
    public class DocumentContainerModel : ContainerModel
    {

        public DocumentContainerModel() : base(new DocumentModel())
        {

            Type = ModelType.Document;

            Title = "Documents";
        }

    }
}
