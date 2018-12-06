using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Shared.Classes;
using System.Linq;
using NEMILTEC.Service.Data.Expressions;

namespace NEMILTEC.Service.Data.Objects.Commands
{
    public class DataUpdateCommand : ADataCommand
    {
        /// <summary>
        /// data for update
        /// </summary>
        public Dictionary<string, object> Values { get; set; }

        /// <summary>
        /// condition expression
        /// </summary>
        public IDataExpression Condition { get; set; }

        public override string ToSQL()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendFormat("UPDATE [{0}]\n", EntityName);


           if (!Values.IsNullOrEmpty())
            {
                strBuilder.AppendFormat("SET ");
                strBuilder.AppendFormat("{0}\n",Values.Select(kvp => String.Format("[{0}] = [{1}]", kvp.Key, ValueExpressionSQLConverter.Convert(kvp.Value))).ToCSV());
            }

            if (Condition != null)
            {
                strBuilder.AppendFormat("WHERE {0}", ((ISQLExpression)Condition).ToSQL());
            }

            return strBuilder.ToString();
        }
    }
}
