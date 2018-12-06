using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using NEMILTEC.Service.Data;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.MVC.Models.Query
{
    [Serializable]
    public class QueryOutputModel
    {
        public QueryOutputModel()
        {
            
        }


            public long ID { get; set; }
            public long QueryID { get; set; }
            public long UserID { get; set; }
            public Nullable<int> ResultType { get; set; }
            public Nullable<int> TotalRows { get; set; }

            public DataTable Data { get; set; }

    }
}
