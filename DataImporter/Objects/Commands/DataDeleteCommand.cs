using System;
using System.Text;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using NEMILTEC.Interfaces.Service.Data.Expressions;

namespace NEMILTEC.Service.Data.Objects.Commands
{
    public class DataDeleteCommand : ADataCommand
    {

        /// <summary>
        /// condition expression
        /// </summary>
        public IDataExpression Condition { get; set; }

        public override string ToSQL()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendFormat("DELETE FROM [{0}]\n", EntityName);

            if (Condition != null)
            {
                strBuilder.AppendFormat("WHERE {0}", ((ISQLExpression)Condition).ToSQL());
            }

            return strBuilder.ToString();
        }
    }
}
