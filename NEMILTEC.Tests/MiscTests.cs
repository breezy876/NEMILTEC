using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NEMILTEC.Shared;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Tests;

namespace NEMILTEC.Tests
{


    [TestClass]
    public class MiscTests
    {


        public interface IModel
        {
             int ID { get; set; }
        }

        public class X : IModel
        {
            public int ID { get; set; }
        }

        public class Y : IModel
        {
            public int ID { get; set; }
        }

        //public MiscTests()
        //{
        //     _first = new List<IModel>() { new X() { ID = 1}, new X() { ID = 2} };
        //     _second = new List<IModel>() { new Y() { ID = 3 }, new Y() { ID = 4 } };

        //    var items = new[] {_first, _second};

        //    var itemsDic = items.ToDictionary(x => x.GetType(), x => x);

        //    _modelMapDic = new Dictionary<Type, List<IModel>>()
        //{
        //    {typeof(X), _first},
        //    {typeof(Y), _second}
        //};
        //}

        List<IModel> _first;
        List<IModel> _second;

        Dictionary<Type, List<IModel>> _modelMapDic;


        [TestMethod]
        public void Test_ModelMapper()
        {

            var model = new X();

            var items = _modelMapDic[model.GetType()];


        }

        public interface IChild
        {
            string Name { get; set; }
        }

        public class Child : IChild
        {
            public string Name { get; set; }
        }

        public class Test
        {
            public Test()
            {
                Col1 = new List<Child>() { new Child() { Name = "Child1"}, new Child() };
                Col2 = new List<Child>();
            }

            public string Prop1 { get; set; }
            public string Prop2 { get; set; }

            public List<Child> Col1 { get; set; }
            public List<Child> Col2 { get; set; }
        }

        [TestMethod]
        public void Test_Reflection1()
        {
  
            var item = new Test() { };
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();

            var childCols = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                var propType = property.PropertyType;
                if (typeof(IEnumerable<IChild>).IsAssignableFrom(propType)) //property is view model child collection
                {
                    var typeName = property.PropertyType.GetGenericArguments();


                }
            }

            var x = 1;
        }


        private bool _EvaluateFunc(TestChild node, Func<TestChild, bool> cond)
        {
            if (cond(node))
                return true;
            if (!node.Children.IsNullOrEmpty())
            {
                foreach(var child in node.Children)
                {
                        if (cond(child))
                            return true;
                    _EvaluateFunc(child, cond);
                }
            }
            return false;

        }

        [TestMethod]
        public void Test_Aggregate()
        {

            var ints = new[] { 1 };
            var agg = ints.Aggregate((x, y) => x ^ y);


        }

        [TestMethod]
        public void Test_CSV()
        {

            var items = new[] {"1"};
            var csv = items.ToCSV();


        }

        [TestMethod]
        public void Test_Dictionary_Intersect()
        {
            var dic1 = new Dictionary<string, object>() {
                        {"test1", 1},
                        {"test2", 2},
                        {"test3", 3}
                    };

            var set1 = new HashSet<KeyValuePair<string, object>>(dic1);


            var dic2 = new Dictionary<string, object>() {
                        {"test2", 1},
                        {"test3", 2}
                    };

            bool overlaps = set1.Overlaps(dic2);
            Assert.IsTrue(overlaps);

        }


        [TestMethod]
        public void Test_Dictionary_Overlaps()
        {
            var dic1 = new Dictionary<string, object>() {
                        {"test1", 1},
                        {"test2", 2},
                        {"test3", 3},
                        {"test4", 4},
                        {"test5", 5}
            };

            var set1 = new HashSet<KeyValuePair<string, object>>(dic1);
        

            var dic2 = new Dictionary<string, object>() {
                        {"test2", 2},
                           {"test5", 5},
                    };

            var newSet = set1.Intersect(dic2);
   

        }

        [TestMethod]
        public void Test_Overlaps()
        {
            var set1 = new HashSet<int>() { 1, 3 };
            var set2 = new[] { 1, 3, 7, 8, 9 };

            bool overlaps = set1.Overlaps(set2);
            Assert.IsTrue(overlaps);

            set1 = new HashSet<int>() { 1, 3, 7, 8, 9 };
            set2 = new[] { 1, 3 };

            overlaps = set1.Overlaps(set2);
            Assert.IsTrue(overlaps);
        }


        [TestMethod]
        public void Test_Itersect()
        {
            var set1 = new HashSet<int>() { 1, 3 };
            var set2 = new[] { 1, 3, 7, 8, 9 };

            var newSet = set1.Intersect(set2);

            set1 = new HashSet<int>() { 1, 3, 7, 8, 9 };
            set2 = new[] { 1, 3 };

            newSet = set1.Intersect(set2);

        }

        [TestMethod]
        public void Test_OrderBy()
        {
            var setDic = new Dictionary<string, int>
            {
                {"1", 1},
                {"2", 2},
                {"3", 3},
                {"4", 4},
                {"5", 5}
            };

            var set = new[] { "1", "4", "2", "5", "3"};

            var ordered = set.OrderBy(i => setDic[i]);

        }


        [TestMethod]
        public void Test_Tree_Condition()
        {
            var tree = new TestTree() {
                Children = new TestChild[]
                {
                    new TestChild()
                    {

                        Index = 1,
                        Name = "First",
                        Children = new TestChild[]
                        {
                            new TestChild()
                            {
                                Name = "First.1"
                            },
                            new TestChild()
                            {
                                Name = "1"
                            },
                            new TestChild()
                            {
                                Name = "1"
                            }
                        }
                    },
                    new TestChild()
                    {
                        Index = 2,
                        Name = "Second",
                          Children = new TestChild[]
                        {
                            new TestChild()
                            {
                                Name = "Second.1"
                            },
                            new TestChild()
                            {
                               Name = "Second.2"
                            }
                        }
                    }
                }
            };

             Func<TestChild, bool> cond = (x) => { return x.Name == "1"; }; 

            var children = tree.Children.Where(c => _EvaluateFunc(c, cond) ).ToArray();


        }

        //[TestMethod]
        //public void Test_KeyValuePair_EqualityComparer_GroupBy()
        //{

        //    var arr = new Test[] {
        //        new Test() { Index = 1, Prop2 = "x", Prop3 = "g" },
        //        new Test() { Index = 2, Prop2 = "x", Prop3 = "g" },
        //        new Test() { Index = 3, Prop2 = "x", Prop3 = "g" },
        //        new Test() { Index = 4, Prop2 = "x", Prop3 = "s" },
        //        new Test() { Index = 5, Prop2 = "y", Prop3 = "g" },
        //        new Test() { Index = 6, Prop2 = "y", Prop3 = "g" },
        //    };

        //    var group = arr.GroupBy(x => new KeyValuePairEqualityComparer() { KeyValuePair = new Dictionary<string, object>() { { "Prop2", x.Prop2 }, { "Prop3", x.Prop3 } } });

        //}
    }
}
