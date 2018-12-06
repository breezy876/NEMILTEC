using System;
using System.Collections.Generic;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    public class DataTableModel
    {
        public string Name { get; set; }
        public IEnumerable<string> Columns { get; set; }
        public IEnumerable<IEnumerable<object>> Values { get; set; }
    }
}
