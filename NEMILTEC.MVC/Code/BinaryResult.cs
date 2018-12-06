using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NEMILTEC.MVC.Code
{
    public class BinaryResult : ActionResult
    {
        public BinaryResult(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.BinaryWrite(Data);
        }
    }
}
