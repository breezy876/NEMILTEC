using System;
using System.Collections.Generic;

namespace NEMILTEC.MVC.Models.IntelliFlows
{
    [Serializable]
    public class IntelliFlowsModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<ProjectModel> Projects { get; set; }
        public IEnumerable<IntelliFlowModel> IntelliFlows { get; set; }
    }
}
