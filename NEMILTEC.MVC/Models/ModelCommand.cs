using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    public class ModelCommand
    {
        public string Url { get; set; }
        public object Data { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public int Id { get; set; }
    }
}
