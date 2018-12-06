using System;

namespace NEMILTEC.Service.Data.Expressions
{
    public static class ValueExpressionSQLConverter
    {

        public static string Convert(object val)
        {
            if (val is string)
                    return String.Format("'{0}'", val);
            else
                    return val.ToString();
            

            return "NULL";

        }


    }
}
