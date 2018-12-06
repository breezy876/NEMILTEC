using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Abstract;
using NEMILTEC.Service.Automation.Concrete.Actions;
using NEMILTEC.Service.Automation.Concrete.Iterators;
using NEMILTEC.Service.Automation.Concrete.Rules;
using NEMILTEC.Service.Data.Database;
using NEMILTEC.Service.Data.File;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Automation.Concrete
{
    /// <summary>
    /// executes the intelliflow items
    /// visitor pattern
    /// </summary>
    public class IntelliFlowItemProcessor 
    {


        
        public IntelliFlowOutput Execute(IntelliFlow intelliFlow)
        {
            foreach (var item in intelliFlow.Items)
            {
                item.Execute();
            }
            return null;
        }

        #region actions

        public IntelliFlowItemOutput Execute(DatabaseCommandIntelliFlowAction action)
        {
            var paramArray = action.Input.Parameters.ToArray();
            string connString = (string)paramArray[0].GetValue(action.Input.Data);
            var db = new Database { ConnectionString = connString };
            var sql = action.Command.ToSQL();
            action.Output.Data = db.ExecuteCommand(sql);
            return _ExecuteChildren(action);
        }

        public IntelliFlowItemOutput Execute(DatabaseQueryIntelliFlowAction action)
        {
            var paramArray = action.Input.Parameters.ToArray();
            string connString = (string)paramArray[0].GetValue(action.Input.Data);
            var queryParams = (IDictionary<string, object>)paramArray[1].GetValue(action.Input.Data);
            var sql = action.Query.ToSQL();
            var db = new Database {ConnectionString = connString};
            var dataTable = db.ExecuteQuery(sql);
            action.Output.Data = dataTable;
            return _ExecuteChildren(action);
        }

        public IntelliFlowItemOutput Execute(ExportDataToFileIntelliFlowAction action)
        {
            var paramArray = action.Input.Parameters.ToArray();
            var fileType = (FileType)paramArray[0].GetValue(action.Input.Data);
            var exporter = DataTableFileFactory.CreateExporter(fileType);
            action.Output.Data = exporter.Export((DataTable)action.Input.Data);
            return _ExecuteChildren(action);
        }

        public IntelliFlowItemOutput Execute(ImportDataFromFileIntelliFlowAction action)
        {
            var paramArray = action.Input.Parameters.ToArray();
            var fileType = (FileType)paramArray[0].GetValue(action.Input.Data);
            var filePath = (string)paramArray[1].GetValue(action.Input.Data);
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var importer = DataTableFileFactory.CreateImporter(fileType);
            action.Output.Data = importer.Import((DataTable)action.Input.Data, stream);
            return _ExecuteChildren(action);
        }
        #endregion


        public IntelliFlowItemOutput Execute(IntelliFlowTrueFalseRule rule)
        {
            var children = rule.Children.ToArray();
            var trueBranch = children[0];
            var falseBranch = children[1];

            var exp = rule.Expressions.First();
            var result = Convert.ToBoolean(exp.Evaluate(rule.Input.Data));
            if (result)
            {
                trueBranch.Input.Data = rule.Input.Data;
                return trueBranch.Execute(); 
            }
            else
            {
                falseBranch.Input.Data = rule.Input.Data;
                return falseBranch.Execute(); 
            }

        }

        public IntelliFlowItemOutput Execute(IntelliFlowCaseRule rule)
        {
            //execute children
            IntelliFlowItemOutput output = null;

            var children = rule.Children.ToArray();
            int index = 0;

            foreach (var exp in rule.Expressions)
            {
                if (Convert.ToBoolean(exp.Evaluate(rule.Input.Data)))
                {
                    var child = children[index];
                    child.Input.Data = rule.Input.Data;
                    return child.Execute();
                }
                index++;
            }

            return output;
        }

        public IntelliFlowItemOutput Execute(IntelliFlowDataSetRowIterator rule)
        {
            throw new NotImplementedException();
        }

        public IntelliFlowItemOutput Execute(IntelliFlowCollectionIterator iter)
        {
            var data = (IEnumerable<object>)iter.Input.Data;
            foreach (var item in data)
            {
                foreach (var child in iter.Children)
                {
                    child.Input.Data = item;
                    var output = child.Execute();
                }
            }

            return null;
        }


        #region private shared methods


        private IntelliFlowItemOutput _ExecuteChildren(AIntelliFlowItem item)
        {
            //execute children
            IntelliFlowItemOutput output = null;

            foreach (var child in item.Children)
            {
                if (item.Output != null)
                {
                    child.Input.Data = item.Output.Data;
                }
                else
                {
                    child.Input.Data = item.Input.Data;
                }
                output = child.Execute();
            }

            return output;
        }

        #endregion
    }
}
