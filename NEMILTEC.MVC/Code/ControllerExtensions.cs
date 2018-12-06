using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NEMILTEC.MVC.Code
{
    public static class ControllerExtensions
    {
        public static BinaryResult Binary(this Controller controller, byte[] data)
        {
            return new BinaryResult(data);
        }

    }
}
