using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.Shared.Classes.Data;
using NEMILTEC.Shared.Enums.Data;

namespace NEMILTEC.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            //var context = new DataContext(false);
            //var dataSvc = new DataAccess(context);
            //var vmContainer = dataSvc.CreateContainerModel((int)ModelType.Report);

            ////TODO should go in JSON template + loaded from file
            //var tableContainer = new TableContainerModel(vmContainer);

            //_UpdateContainer(tableContainer);

            var data = new DataEntity(
                new List<DataProperty>()
                {
                    new DataProperty() {Name = "Property1", Value = 1, Type = DataType.Number},
                    new DataProperty() {Name = "Property2", Value = 2, Type = DataType.Number},
                    new DataProperty() {Name = "Property3", Value = "Test", Type = DataType.String}
                });

            var vmPage = new PageModel()
            {
                //Test = new DataEntity() {
                //    Properties = new Dictionary<string, DataProperty>()
                //    {
                //        { "Test1", new DataProperty() { Name = "Test1", Type=DataType.Number, Value=2222} }
                //    }
                //},
                Title = "Home",
                Children = new List<IModel>() { new SegmentContainerModel(new DataContainerModel()
                {
                    Title = "Test Data",
                    IsEditable = true,
                }) }
            };

            return View(vmPage);
        }
    }
}