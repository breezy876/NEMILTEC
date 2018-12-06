//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NEMILTEC.Service.Data.Expressions.Data;
//using NEMILTEC.Service.Data.Expressions.Evaluators;
//using NEMILTEC.Service.Data.Expressions.Query;
//using NEMILTEC.Service.Data.Objects.Queries;
//using NEMILTEC.Shared.Classes.Data;

//namespace NEMILTEC.Tests.NEMILTECT.Data
//{

//    [TestClass]
//    public class DataQueryTests
//    {

//        private DataSet _dataSet;

//        private Shared.Classes.Data.DataTable _projectTable;
//        private Shared.Classes.Data.DataTable _planTable;
//        private Shared.Classes.Data.DataTable _taskTable;

//        private DataColumn[] _projectCols;
//        private DataColumn[] _planCols;
//        private DataColumn[] _taskCols;

//        private DataRow[] _projectRows;
//        private DataRow[] _planRows;
//        private DataRow[] _taskRows;

//        private DataRelation _projectPlanRelations;
//        private DataRelation _projectTaskRelations;
//        private DataRelation _planRelations;


//        #region private methods


//        private DataColumn[] _BuildColumns(Shared.Classes.Data.DataTable table, string[] columnNames)
//        {
//            int index = 0;
//            return columnNames.Select(c =>
//                new DataColumn()
//                {
//                    DataTable = table,
//                    DataSet = table.DataSet,
//                    Name = c,
//                    Index = index++
//                }).ToArray();
//        }

//        private DataRow[] _BuildRows(Shared.Classes.Data.DataTable table, DataColumn[] columns, object[][] values)
//        {

//            var totalRows = values.Length;
//            var totalCols = values[0].Length;

//            var rows = new List<DataRow>();

//            for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
//            {
//                var row = new DataRow()
//                {
//                    DataSet = table.DataSet,
//                    DataTable = table,
//                    Key = (int)values[rowIndex][0],
//                    Index = rowIndex
//                };
//                var cells = new DataCell[totalCols];
//                for (int colIndex = 0; colIndex < totalCols; colIndex++)
//                {
//                    cells[colIndex] = new DataCell()
//                    {
//                        DataSet = table.DataSet,
//                        DataTable = table,
//                        Column = columns[colIndex],
//                        ColumnName = columns[colIndex].FullName,
//                        ColumnIndex = colIndex,
//                        RowIndex = rowIndex,
//                        Row = row,
//                        TableName = table.Name,
//                        Value = values[rowIndex][colIndex]
//                    };
//                }
//                row.Cells = cells;
//                rows.Add(row);
//            }
//            return rows.ToArray();

//        }
//        #endregion

//        [TestInitialize]
//        public void Initialize()
//        {
//            _dataSet = new DataSet();

//            _projectTable = new Shared.Classes.Data.DataTable()
//            {
//                DataSet = _dataSet,
//                Name = "Project"
//            };

//            _planTable = new Shared.Classes.Data.DataTable()
//            {
//                DataSet = _dataSet,
//                Name = "ProjectPlan"
//            };

//            _taskTable = new Shared.Classes.Data.DataTable()
//            {
//                DataSet = _dataSet,
//                Name = "Task"
//            };

//            _projectCols = _BuildColumns(_projectTable, new[] { "ProjectID", "Name" });
//            _planCols = _BuildColumns(_planTable, new[] { "PlanID", "Name", "Project" });
//            _taskCols = _BuildColumns(_taskTable, new[] { "TaskID", "Name", "Plan", "Project", "IsMilestone", "Cost" });

//            var projectData = new object[][]
//            {
//                new object[] {100, "Project1"},
//                new object[] {200, "Project2"}
//            };

//            var planData = new object[][]
//            {
//                new object[] {10, "Plan1", 100},
//                new object[] {11, "Plan2", 100},
//                new object[] {12, "Plan3", 200}
//                //new object[] {12, "Plan3", 100}
//            };

//            var taskData = new object[][]
//            {
//                new object[] {1, "Task1", 10, 100, false, 0},
//                new object[] {2, "Task2", 10, 100, false, 0},
//                new object[] {3, "Task3", 11, 100, false, 0},
//                new object[] {4, "Task4", 12, 200, false, 100},
//                new object[] {5, "Task5", 12, 200, false, 200},
//                new object[] {6, "Milestone1", 10, 100, true, 1000},
//                //new object[] {3, "Task3", 0},
//            };

//            _projectRows = _BuildRows(_projectTable, _projectCols, projectData);
//            _planRows = _BuildRows(_planTable, _planCols, planData);
//            _taskRows = _BuildRows(_taskTable, _taskCols, taskData);

//            _projectPlanRelations =
//                new DataRelation()
//                {
//                    ParentTable = _projectTable,
//                    ChildTable = _planTable,
//                    ParentColumn = _projectCols[0],
//                    ChildColumn = _planCols[2],
//                };

//            _projectTaskRelations =
//            new DataRelation()
//            {
//                ParentTable = _projectTable,
//                ChildTable = _taskTable,
//                ParentColumn = _projectCols[0],
//                ChildColumn = _taskCols[3],
//            };

//            _planRelations =
//                new DataRelation()
//                {
//                    ParentTable = _planTable,
//                    ChildTable = _taskTable,
//                    ParentColumn = _planCols[0],
//                    ChildColumn = _taskCols[2],
//                };


//            _dataSet.Tables = new Dictionary<string, Shared.Classes.Data.DataTable> {
//                                { _projectTable.Name, _projectTable },
//                                { _planTable.Name, _planTable },
//                                { _taskTable.Name, _taskTable}
//                            };

//            _dataSet.Relations = new[] {
//                                _projectPlanRelations,
//                               _planRelations,
//                                _projectTaskRelations
//                            };

//            _projectTable.Rows = _projectRows;
//            _projectTable.Columns = _projectCols;
//            _projectTable.ColumnNames = _projectTable.Columns.Select(c => c.FullName).ToArray();

//            _planTable.Rows = _planRows;
//            _planTable.Columns = _planCols;
//            _planTable.ColumnNames = _planTable.Columns.Select(c => c.FullName).ToArray();

//            _taskTable.Rows = _taskRows;
//            _taskTable.Columns = _taskCols;
//            _taskTable.ColumnNames = _taskTable.Columns.Select(c => c.FullName).ToArray();

//            var parentRows = _taskTable.Rows.ToArray()[0].ParentRows;
//            var childRows = _projectTable.Rows.ToArray()[0].ChildRows;

//            //var max = _GetTotalRelations(_taskTable.Rows.ToArray()[0]);

//        }


//        [TestMethod]
//        public void Test_DataQueryExpressionVisitor_Evaluate_DataSetQueryExpression_WithProjections()
//        {
//            var dataQueryExpressionVisitor = new DataQueryExpressionEvaluator();

//            var expression = new DataSetQueryExpression()
//            {
//                Query = new DataQuery()
//                {
//                    Items = new QueryItems()
//                    {

//                        Projections = new[] {

//                            new DataQueryFieldParameter() {
//                                Expression = new DataRowFieldExpression() {
//                                EntityName = "Project",
//                                FieldName = "ProjectID" },
//                                EntityName = "Project",
//                                FieldName = "ProjectID"
//                            },

//                            new DataQueryFieldParameter() {
//                                Expression = new DataRowFieldExpression() {
//                                EntityName = "Project",
//                                FieldName = "Name" },
//                                EntityName = "Project",
//                                FieldName = "Name"
//                            },

//                            new DataQueryFieldParameter() {
//                                Expression = new DataRowFieldExpression() {
//                                EntityName = "ProjectPlan",
//                                FieldName = "PlanID" },
//                                EntityName = "ProjectPlan",
//                                FieldName = "PlanID"
//                            },

//                            new DataQueryFieldParameter() {
//                                Expression = new DataRowFieldExpression() {
//                                EntityName = "ProjectPlan",
//                                FieldName = "Name" },
//                                EntityName = "ProjectPlan",
//                                FieldName = "Name"
//                            },

//                            new DataQueryFieldParameter() {
//                                Expression = new DataRowFieldExpression() {
//                                EntityName = "Task",
//                                FieldName = "TaskID" },
//                                EntityName = "Task",
//                                FieldName = "TaskID"
//                            },

//                            new DataQueryFieldParameter() {
//                                Expression = new DataRowFieldExpression() {
//                                EntityName = "Task",
//                                FieldName = "Name" },
//                                EntityName = "Task",
//                                FieldName = "Name"
//                            }
//                        }
//                    }
//                }
//            };


//            var dataTable = dataQueryExpressionVisitor.Evaluate(expression, _dataSet);
//        }

//        //[TestMethod]
//        //public void Test_DataQueryExpressionVisitor_Evaluate_DataQueryExpression_WithProjectionsAndCondition()
//        //{
//        //    var dataQueryExpressionVisitor = new DataQueryExpressionEvaluator();
//        //    var binaryExpressionVisitor = new BinaryExpressionEvaluator();

//        //    var expression = new DataQueryExpression()
//        //    {
//        //        Condition = new BinaryEqualsExpression(binaryExpressionVisitor)
//        //        {
//        //            LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor)
//        //            {
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "PlanID"
//        //            },
//        //            RightExpression = new ValueExpression() { Value = 11 }
//        //        },

//        //        Projections = new[] {

//        //            new DataQueryFieldParameter() {
//        //                Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "Project",
//        //                FieldName = "ProjectID" },
//        //                EntityName = "Project",
//        //                FieldName = "ProjectID",
//        //            },

//        //            new DataQueryFieldParameter() {
//        //                  Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "Project",
//        //                FieldName = "Name" },
//        //                EntityName = "Project",
//        //                FieldName = "Name",
//        //            },


//        //            new DataQueryFieldParameter() {
//        //                Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "ProjectPlan",
//        //                FieldName = "PlanID" },
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "PlanID",
//        //            },
//        //            new DataQueryFieldParameter() {
//        //                  Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "ProjectPlan",
//        //                FieldName = "Name" },
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "Name",
//        //            },

//        //            new DataQueryFieldParameter() {
//        //                                          Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "Task",
//        //                FieldName = "TaskID" },
//        //                EntityName = "Task",
//        //                FieldName = "TaskID",
//        //            },
//        //            new DataQueryFieldParameter() {
//        //                  Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "Task",
//        //                FieldName = "Name" },
//        //                EntityName = "Task",
//        //                FieldName = "Name",
//        //            }
//        //        }
//        //    };


//        //    dataQueryExpressionVisitor.Evaluate(expression, _taskTable);
//        //}


//        //[TestMethod]
//        //public void Test_DataQueryExpressionVisitor_Evaluate_DataQueryExpression_WithGroupingsAndAggregates()
//        //{
//        //    var dataQueryExpressionVisitor = new DataQueryExpressionEvaluator();
//        //    var binaryExpressionVisitor = new BinaryExpressionEvaluator();
//        //    var functionExpressionVisitor = new FunctionExpressionEvaluator();

//        //    var expression = new DataQueryExpression()
//        //    {
//        //        Query = new DataQuery()
//        //        {
//        //            Items = QueryItems()
//        //        {
    
//        //        }
//        //        }
//        //        //Condition = new BinaryEqualsExpression(binaryExpressionVisitor)
//        //        //{
//        //        //    LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor)
//        //        //    {
//        //        //        EntityName = "ProjectPlan",
//        //        //        FieldName = "PlanID"
//        //        //    },
//        //        //    RightExpression = new ValueExpression() { Value = 11 }
//        //        //},

//        //        Projections = new[] {

//        //            //                    new DataQueryFieldParameter() {
//        //            //    Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) { 
//        //            //        EntityName = "Project",
//        //            //    FieldName = "ProjectID" },
//        //            //    EntityName = "Project",
//        //            //    FieldName = "ProjectID",
//        //            //},

//        //            new DataQueryFieldParameter() {
//        //                  Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "Project",
//        //                FieldName = "Name" },
//        //                EntityName = "Project",
//        //                FieldName = "Name",
//        //            },


//        //            new DataQueryFieldParameter() {
//        //                Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "ProjectPlan",
//        //                FieldName = "PlanID" },
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "PlanID",
//        //            },

//        //            new DataQueryFieldParameter() {
//        //                  Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "ProjectPlan",
//        //                FieldName = "Name" },
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "Name",
//        //            },
//        //            //new DataQueryFieldParameter() {
//        //            //      Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) { 
//        //            //        EntityName = "Task",
//        //            //    FieldName = "TaskID" },
//        //            //    EntityName = "Task",
//        //            //    FieldName = "TaskID",
//        //            //},
//        //            new DataQueryFieldParameter() {
//        //                  Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "Task",
//        //                FieldName = "Name" },
//        //                EntityName = "Task",
//        //                FieldName = "Name",
//        //            },
//        //        },

//        //        Groupings = new[] {

//        //            new DataQueryFieldParameter() {
//        //                Expression = new DataRowFieldExpression(dataQueryExpressionVisitor) {
//        //                    EntityName = "ProjectPlan",
//        //                FieldName = "Name" },
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "Name",
//        //            }
//        //        },

//        //        Aggregates = new[] {

//        //            new DataQueryFieldParameter() {
//        //                Expression = new SumFunction(functionExpressionVisitor)
//        //                    {
//        //                        Expressions = new [] {
//        //                            new DataQueryAggregateFieldExpression(dataQueryExpressionVisitor) {
//        //                            EntityName = "ProjectPlan",
//        //                            FieldName = "PlanID" }
//        //                        }
//        //                    },
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "PlanID" },
//        //        },

//        //        AggregateCondition = new BinaryGreaterThanOrEqualsExpression(binaryExpressionVisitor)
//        //        {
//        //            LeftExpression = new DataRowFieldExpression(dataQueryExpressionVisitor)
//        //            {
//        //                EntityName = "ProjectPlan",
//        //                FieldName = "PlanID"
//        //            },
//        //            RightExpression = new ValueExpression() { Value = 10 }
//        //        },

//        //    };


//        //    var table = (DataTable)dataQueryExpressionVisitor.Evaluate(expression, _dataSet);
//        //}



//        //[TestMethod]
//        //public void Test_DataQueryRowGrouping_GroupBy()
//        //{

//        //    //var arr = new Test[] {
//        //    //    new Test() { Index = 1, Prop2 = "x", Prop3 = "g" },
//        //    //    new Test() { Index = 2, Prop2 = "x", Prop3 = "g" },
//        //    //    new Test() { Index = 3, Prop2 = "x", Prop3 = "g" },
//        //    //    new Test() { Index = 4, Prop2 = "x", Prop3 = "s" },
//        //    //    new Test() { Index = 5, Prop2 = "y", Prop3 = "g" },
//        //    //    new Test() { Index = 6, Prop2 = "y", Prop3 = "g" },
//        //    //};

//        //    //var group = arr.GroupBy(
//        //    //    x =>
//        //    //        new DataQueryRowGrouping(
//        //    //            new KeyValuePairEqualityComparer() { KeyValuePair = new Dictionary<string, object>() { { "Prop2", x.Prop2 }, { "Prop3", x.Prop3 } } }
//        //    //        )).ToArray();
//        //}



//    }
//}

