using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NEMILTEC.Domain;

namespace NEMILTEC.MVC.Models.IntelliFlows
{
    [Serializable]
    public class IntelliFlowModel :Model
    {
        public IntelliFlowModel()
        {

        }

        public string Description { get; set; }


 
        public long? CategoryId { get; set; }

        public CategoryModel Category { get; set; }



        public long? ProjectId { get; set; }

        public ProjectModel Project { get; set; }



    }
}
