//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq.Dynamic;
//using System.Linq;
//using System.Collections.Generic;
//using NEMILTEC.Shared;
//using NEMILTEC.Data.Concrete.Data.Expressions.Binary;
//using NEMILTEC.Data.Concrete.Data.Expressions.Query;
//using NEMILTEC.Data.Concrete.Data.Expressions;
//using NEMILTEC.Data.Concrete.Data.Expressions.Evaluators;
//using NEMILTEC.Data.Concrete.Data.Expressions.Functions.Math;
//using NEMILTEC.Data.Concrete.Data.Expressions.Functions.String;
//using NEMILTEC.Data.Concrete.Data.Expressions.Binary.Math;
//using NEMILTEC.Data.Abstract.Data;
//using NEMILTEC.Data.Concrete.Data.Expressions.Data;

//namespace NEMILTEC.Tests
//{

//    [TestClass]
//    public class Expressions
//    {

//        [TestMethod]
//        public void Test_Evaluate_Result_Correct()
//        {

//            //simple expression
//            var dataQueryExpressionVisitor = new DataQueryExpressionEvaluator();
//            var binaryExpressionVisitor = new BinaryExpressionEvaluator();
//            var functionExpressionVisitor = new FunctionExpressionEvaluator();

//            IDataExpression expression = new BinaryAndExpression(binaryExpressionVisitor)
//            {
//                LeftExpression = new BinaryLessThanExpression(binaryExpressionVisitor)
//                {
//                    LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column1" },
//                    RightExpression = new ValueExpression() { Value = 5 }
//                },

//                RightExpression = new BinaryGreaterThanExpression(binaryExpressionVisitor)
//                {
//                    LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column2" },
//                    RightExpression = new ValueExpression() { Value = 3 }
//                }
//            };

//            string sql = null;

//            //sql = binaryExpressionVisitor.ToSQL((BinaryAndExpression)expression);


//            ////more complex expression
//            //expression = new BinaryAndExpression(binaryExpressionVisitor)
//            //{
//            //    LeftExpression = new BinaryAndExpression(binaryExpressionVisitor)
//            //    {
//            //        LeftExpression = new BinaryLessThanExpression(binaryExpressionVisitor)
//            //        {
//            //            LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column1" },
//            //            RightExpression = new ValueExpression() { Value = 5 }
//            //        },

//            //        RightExpression = new BinaryGreaterThanExpression(binaryExpressionVisitor)
//            //        {
//            //            LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column2" },
//            //            RightExpression = new ValueExpression() { Value = 3 }
//            //        }
//            //    },
//            //    RightExpression = new BinaryEqualsExpression(binaryExpressionVisitor)
//            //    {
//            //        LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column3" },
//            //        RightExpression = new ValueExpression() { Value = "test" }
//            //    }
//            //};

//            //sql = binaryExpressionVisitor.ToSQL((BinaryAndExpression)expression);


//            //simple function expression
//            expression = new ConcatFunction(functionExpressionVisitor)
//            {
//                Expressions = new IDataExpression[] {
//                    new ValueExpression() { Value = "Test1"},
//                    new ValueExpression() { Value = "_Test2"}
//                }
//            };

//            sql = functionExpressionVisitor.ToSQL((ConcatFunction)expression);

//            //complex function expression
//            expression = new ConcatFunction(functionExpressionVisitor)
//            {
//                Expressions = new IDataExpression[] {
//                    new LeftFunction(functionExpressionVisitor) {
//                        Expressions = new IDataExpression[] {
//                             new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column1" },
//                             new BinarySubtractExpression(binaryExpressionVisitor)
//                                {
//                                    LeftExpression = new ValueExpression() { Value = 5 },
//                                    RightExpression = new ValueExpression() { Value = 2 }
//                                }
//                        }
//                    },
//                    new ValueExpression() { Value = "_Test"}
//                }
//            };

//            sql = functionExpressionVisitor.ToSQL((ConcatFunction)expression);



//        }
//        [TestMethod]
//        public void Test_ToSQL_Result_Correct()
//        {

//            //simple expression
//            var dataQueryExpressionVisitor = new DataQueryExpressionEvaluator();
//            var binaryExpressionVisitor = new BinaryExpressionEvaluator();
//            var functionExpressionVisitor = new FunctionExpressionEvaluator();

//            IDataExpression expression = new BinaryAndExpression(binaryExpressionVisitor)
//            {
//                LeftExpression = new BinaryLessThanExpression(binaryExpressionVisitor)
//                {
//                    LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column1" },
//                    RightExpression = new ValueExpression() { Value = 5 }
//                },

//                RightExpression = new BinaryGreaterThanExpression(binaryExpressionVisitor)
//                {
//                    LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column2" },
//                    RightExpression = new ValueExpression() { Value = 3 }
//                }
//            };

//            string sql = null;

//            //sql = binaryExpressionVisitor.ToSQL((BinaryAndExpression)expression);


//            ////more complex expression
//            //expression = new BinaryAndExpression(binaryExpressionVisitor)
//            //{
//            //    LeftExpression = new BinaryAndExpression(binaryExpressionVisitor)
//            //    {
//            //        LeftExpression = new BinaryLessThanExpression(binaryExpressionVisitor)
//            //        {
//            //            LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column1" },
//            //            RightExpression = new ValueExpression() { Value = 5 }
//            //        },

//            //        RightExpression = new BinaryGreaterThanExpression(binaryExpressionVisitor)
//            //        {
//            //            LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column2" },
//            //            RightExpression = new ValueExpression() { Value = 3 }
//            //        }
//            //    },
//            //    RightExpression = new BinaryEqualsExpression(binaryExpressionVisitor)
//            //    {
//            //        LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column3" },
//            //        RightExpression = new ValueExpression() { Value = "test" }
//            //    }
//            //};

//            //sql = binaryExpressionVisitor.ToSQL((BinaryAndExpression)expression);


//            //simple function expression
//            expression = new ConcatFunction(functionExpressionVisitor)
//            {
//                Expressions = new IDataExpression[] {
//                    new ValueExpression() { Value = "Test1"},
//                    new ValueExpression() { Value = "_Test2"}
//                }
//            };

//            sql = functionExpressionVisitor.ToSQL((ConcatFunction)expression);

//            //complex function expression
//            expression = new ConcatFunction(functionExpressionVisitor)
//            {
//                Expressions = new IDataExpression[] {
//                    new LeftFunction(functionExpressionVisitor) {
//                        Expressions = new IDataExpression[] {
//                             new DataRowFieldExpression(dataQueryExpressionVisitor) { FieldName = "Column1" },
//                             new BinarySubtractExpression(binaryExpressionVisitor)
//                                {
//                                    LeftExpression = new ValueExpression() { Value = 5 },
//                                    RightExpression = new ValueExpression() { Value = 2 }
//                                }
//                        }
//                    },
//                    new ValueExpression() { Value = "_Test"}
//                }
//            };

//            sql = functionExpressionVisitor.ToSQL((ConcatFunction)expression);


//        }
//    }
//}
