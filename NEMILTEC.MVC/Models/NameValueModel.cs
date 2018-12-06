using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    public class NameValueModel
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
