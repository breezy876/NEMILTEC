using System;

namespace NEMILTEC.MVC.Models.IntelliFlows
{
    [Serializable]
    public class IntelliFlowItemModel
    {
        public IntelliFlowItemModel()
        {

        }

        public Nullable<long> IntelliFlowID { get; set; }
        public Nullable<long> ProjectID { get; set; }
        public Nullable<long> ParentItemID { get; set; }
        public string Description { get; set; }
        public Nullable<long> CategoryID { get; set; }


    }
}
