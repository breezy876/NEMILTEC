//using NEMILTEC.Domain;
//using NEMILTEC.Interfaces;
//using NEMILTEC.MVC.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using NEMILTEC.Interfaces.Data;

//namespace NEMILTEC.MVC.Controllers
//{
//    public class ProjectController : Controller
//    {

//        IGenericRepository<Project> _projects;

//        // GET: Project
//        public ActionResult Index()
//        {
//            var projectsVm = _projects.GetAll().Select(p => new ProjectModel(p));

//            return View(projectsVm);
//        }

//        public ProjectController()
//        {
//            _projects = new ListRepository<Project>(Common.Projects);
//        }

//        public JsonResult GetAll()
//        {
//            var projectsVm = _projects.GetAll().Select(p => new ProjectModel(p));
//            return Json(projectsVm, JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult Add(Project item)
//        {
//            _projects.AddOrUpdate(item);
//            return Json(_projects.GetAll(), JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult Remove(int itemId)
//        {
//            _projects.Remove(itemId);
//            return Json(_projects.GetAll(), JsonRequestBehavior.AllowGet);
//        }

//    }
//}