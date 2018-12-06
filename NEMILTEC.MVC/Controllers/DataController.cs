using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Data.File;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.MVC.Code;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Service.Data.File.Exporters;
using NEMILTEC.Service.Data.File.Importers;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.MVC.Controllers
{
    public class DataController : Controller
    {
        [HttpPost]
        public JsonResult SaveData(long parentId, int type, string dataJSON, Shared.Enums.Data.DataType dataType)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);

            bool bResult = dataSvc.SaveData(parentId, type, dataJSON, dataType);

            return Json(bResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAll(int type, long? startIndex, long? total, bool addBlank = true)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);

            var vmCol = dataSvc.GetAll(type, startIndex, total, addBlank);

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get(int type, long id)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);

            var vm = dataSvc.Get(type, id);

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddOrUpdate(int type, string itemJSON)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);

            var vmCol = dataSvc.AddOrUpdate(type, itemJSON);

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remove(int type, string itemJSON)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);

            var vmCol = dataSvc.Remove(type, itemJSON);

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddOrUpdateAll(int type, IEnumerable<string> itemJSONCol)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);

            var vmCol = dataSvc.AddOrUpdate(type, itemJSONCol);

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveAll(int type, IEnumerable<string> itemJSONCol)
        {
            var context = new DataContext(false);
            var dataSvc = new DataAccess(context);

            var vmCol = dataSvc.Remove(type, itemJSONCol);

            return Json(vmCol, JsonRequestBehavior.AllowGet);
        }


        //TODO add other formats other than excel
        [HttpPost]
        public JsonResult Export(string name, DataTable data)
        {
            IFileExporter<DataTable> exporter = new ExcelDataTableFileExporter();

            var fileStream = exporter.Export(data);
            fileStream.Position = 0;

            var ms = new MemoryStream();
            fileStream.CopyTo(ms);

            ms.Position = 0;

            string fileGUID = Guid.NewGuid().ToString();

            Session[fileGUID] = ms.ToArray();

            string fileName = String.Format("{0}_{1}", name, fileGUID);

            return Json( new { FileGUID = fileGUID, FileName = fileName});
        }

        //TODO security
        [HttpGet]
        public FileResult DownloadFile(string fileGUID, string fileName)
        {
            if (Session[fileGUID] != null)
            {
                byte[] data = Session[fileGUID] as byte[];
                return File(data, "application/vnd.ms-excel", fileName + ".xlsx");
            }
            else
            {
                return null;
            }

        }



    }
}