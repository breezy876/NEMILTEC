//using NEMILTEC.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using NEMILTEC.Domain;
//using NEMILTEC.Interfaces.Data;
//using NEMILTEC.MVC.Models;

//namespace NEMILTEC.MVC.Controllers
//{
//    public class IntelliFlowController : Controller
//    {


//        private IGenericRepository<IntelliFlow> _intelliflows;
//        private IGenericRepository<IntelliFlowItem> _intelliflowItems;

//        private Dictionary<long, ProjectModel> _projectsVm;
//        private Dictionary<long, CategoryModel> _categoriesVm;
        

//        public IntelliFlowController()
//        {
//            _intelliflows = new ListRepository<IntelliFlow>(Common.IntelliFlows);
//            _intelliflowItems = new ListRepository<IntelliFlowItem>(Common.IntelliFlowItems);

//            _projectsVm = Common.Projects.Select(p => new ProjectModel(p)).ToDictionary(p => p.ID, p => p);
//            _categoriesVm = Common.Categories.Select(c => new CategoryModel(c)).ToDictionary(c => c.ID, c => c);
//        }

//        private IntelliFlow _CreateModel(IntelliFlowModel vm)
//        {
//            var model = new IntelliFlow()
//            {
//                Name = vm.Name,
//                Description = vm.Description,
//                ID = vm.ID,
//                ProjectID = vm.Project.ID,
//                CategoryID = vm.Category.ID
//            };

//            //if (model.ID == 0)
//            //{
//            //    model.ID = _intelliflows.GetAll().Max(x => x.ID) + 1;
//            //}

//            return model;
//        }

//        private IntelliFlowItem _CreateModel(IntelliFlowItemModel vm)
//        {
//            var model = new IntelliFlowItem()
//            {
//                IntelliFlowID = vm.IntelliFlowID,
//                Name = vm.Name,
//                Description = vm.Description,
//                ID = vm.ID,
//            };

//            //if (model.ID == 0)
//            //{
//            //    model.ID = _intelliflowItems.GetAll().Max(x => x.ID) + 1;
//            //}

//            return model;
//        }

//        private IEnumerable<IntelliFlowModel> _GetIntelliFlows()
//        {

//            var result = _intelliflows.GetAll().Select(i => new IntelliFlowModel(i)
//            {
//                Project = i.ProjectID.HasValue ? _projectsVm[i.ProjectID.Value] : null,
//                Category = i.CategoryID.HasValue ? _categoriesVm[i.CategoryID.Value] : null
//            }).ToList();

//            result.Add(new IntelliFlowModel()
//            {
//                Name = string.Empty,
//                Description = string.Empty,
//                Project = _projectsVm.Values.First(),
//                Category = _categoriesVm.Values.First(),
//                IsNew = true
//            });

//            return result;

//        }

//        private IEnumerable<IntelliFlowItemModel> _GetItems(long parentId, long? parentItemId)
//        {
//            List<IntelliFlowItemModel> result = new List<IntelliFlowItemModel>();

//            if (!parentItemId.HasValue)
//            {
//                result = _intelliflowItems.Query(
//                    i => (i.IntelliFlowID.HasValue && i.IntelliFlowID.Value == parentId) && 
//                    !i.ParentItemID.HasValue)
//                    .Select(i => new IntelliFlowItemModel(i)
//                    {

//                    }).ToList();
//            }
//            else 
//            {
//                result = _intelliflowItems.Query(
//                   i => (i.IntelliFlowID.HasValue && i.IntelliFlowID.Value == parentId) && 
//                   i.ParentItemID.Value == parentItemId.Value)
//                   .Select(i => new IntelliFlowItemModel(i)
//                   {

//                   }).ToList();
//            }

//            result.Add(new IntelliFlowItemModel()
//            {
//                Name = string.Empty,
//                Description = string.Empty,
//                IntelliFlowID = parentId, 
//                ParentItemID = parentItemId,
//                IsNew = true
//            });

//            return result;

//        }

//        // GET: Project
//        public ActionResult Index()
//        {

//            var model = new IntelliFlowsModel()
//            {
//                IntelliFlows = _GetIntelliFlows(),
//                Categories = _categoriesVm.Values,
//                Projects = _projectsVm.Values
//            };

//            return View(model);
//        }


//        public JsonResult GetItems(long parentId)
//        {
//            var itemsVm = _GetItems(parentId, null).ToList();

//            return Json(itemsVm, JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult GetChildItems(long parentId, long parentItemId)
//        {
//            var itemsVm = _GetItems(parentId, parentItemId).ToList();

//            return Json(itemsVm, JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult GetAll()
//        {
//            var intelliFlowsVm = _GetIntelliFlows();
//            return Json(intelliFlowsVm, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public JsonResult AddOrUpdate(IntelliFlowModel item)
//        {
//            var model = _CreateModel(item);
  
//            _intelliflows.AddOrUpdate(model);
//            return Json(_GetIntelliFlows(), JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public JsonResult AddOrUpdateItem(IntelliFlowItemModel item)
//        {
//            var model = _CreateModel(item);
           
//            _intelliflowItems.AddOrUpdate(model);
//            return Json(_GetItems(item.IntelliFlowID.Value, item.ParentItemID), JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult Remove(long id)
//        {
//            _intelliflows.Remove(id);
//            return Json(_GetIntelliFlows(), JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult RemoveItem(long parentId, long? parentItemId, long itemId)
//        {
//            _intelliflowItems.Remove(itemId);

//            var vmItems = _GetItems(parentId, parentItemId);

//            return Json(vmItems, JsonRequestBehavior.AllowGet);
//        }

//    }
//}