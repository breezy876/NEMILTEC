using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEMILTEC.Domain;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.Shared.Classes.Data;
using DataType = NEMILTEC.Shared.Enums.Data.DataType;

namespace NEMILTEC.MVC.Controllers
{
    public class ExpressionController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {


            var vmPage = new PageModel()
            {
                //Test = new DataEntity() {
                //    Properties = new Dictionary<string, DataProperty>()
                //    {
                //        { "Test1", new DataProperty() { Name = "Test1", Type=DataType.Number, Value=2222} }
                //    }
                //},
                Title = "Expressions",
                //Children = new List<IModel>() { new SegmentContainerModel(new DataContainerModel()
                //{
                //    Title = "Test Data",
                //    IsEditable = true,
                //}) }
            };

            return View(vmPage);
        }
    }
}