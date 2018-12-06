using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data
{
    public static class Extensions
    {
        #region sql
        public static string GetTableName(this string fullName)
        {
            if (fullName.IsNullOrEmpty())
                return null;
            var tokens = fullName.Split('.');
            if (tokens.IsNullOrEmpty())
                return null;
            return tokens[0];
        }

        public static string GetFieldName(this string fullName)
        {
            if (fullName.IsNullOrEmpty())
                return null;
            var tokens = fullName.Split('.');
            if (tokens.IsNullOrEmpty())
                return null;
            return tokens[1];
        }


        #endregion
    }
}
