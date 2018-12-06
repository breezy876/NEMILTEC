using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models
{
    public class TestModel : Model
    {
        public TestModel(string[] data)
        {
            Title = data[0];
            Property1 = data[1];
            Property2 = data[2];
            Property3 = data[3];
        }

        public TestModel()
        {
            Title = "Test";
        }

        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }

    }
}
