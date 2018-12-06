using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Query
{
    [Serializable]
    public class QueryOrderingModel
    {

        public QueryOrderingModel()
        {


        }

        public long ID { get; set; }
        public long QueryFieldParameterID { get; set; }
        public int OrderType { get; set; }
    }
}
