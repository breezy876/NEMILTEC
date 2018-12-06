using System;
using System.Text;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Objects.Queries
{
    public class DataQueryFieldParameter
    {

        public string Name { get; set; }

        public string EntityName { get; set; }
        public string FieldName { get; set; }

        public IDataExpression Expression { get; set; }

        public override string ToString()
        {
            var outStr = new StringBuilder();

            if (EntityName.IsNullOrEmpty())
            {
                outStr.AppendFormat("[{0}]", FieldName);
            }

            else
            {
                outStr.AppendFormat("[{0}].[{1}]", EntityName, FieldName);
            }

            if (!Name.IsNullOrEmpty())
            {
                outStr.AppendFormat(" AS '{0}'", Name);

            }

            return outStr.ToString();

        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            var kvp = (DataQueryFieldParameter)obj;

            return this.GetHashCode() == kvp.GetHashCode();

        }
    }
}
