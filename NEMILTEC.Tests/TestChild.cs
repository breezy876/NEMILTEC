using System.Collections.Generic;

namespace NEMILTEC.Tests
{
    public class TestChild
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public IEnumerable<TestChild> Children { get; set;}
    }
}