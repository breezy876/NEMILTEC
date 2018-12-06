using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Service.Data.Expressions;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Objects.Commands
{
    public class DataInsertCommand : ADataCommand
    {
        /// <summary>
        /// data for insert
        /// </summary>
        public IDictionary<string, object> Values { get; set; }


        public override string ToSQL()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendFormat("INSERT INTO [{0}] ({1}) VALUES ({2})\n", 
                EntityName, 
                Values.Select(kvp => String.Format("[{0}]", kvp.Key)).ToCSV(),
                Values.Select(kvp => String.Format("{0}", ValueExpressionSQLConverter.Convert(kvp.Value))).ToCSV());

            return strBuilder.ToString();
        }
    }
}
