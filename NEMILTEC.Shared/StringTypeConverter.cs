using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.Shared.Classes
{
    public static class StringTypeConverter
    {

        private static IDictionary<NEMILTEC.Shared.Enums.Data.DataType, Func<string, object>> _converters = new Dictionary
            <NEMILTEC.Shared.Enums.Data.DataType, Func<string, object>>()
        {
            {NEMILTEC.Shared.Enums.Data.DataType.Number, (x) => Int64.Parse(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Decimal, (x) => Decimal.Parse(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Boolean, (x) => Boolean.Parse(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.String, (x) => x},
        };

        public static object Convert(NEMILTEC.Shared.Enums.Data.DataType type, string strVal)
        {
            return _converters[type](strVal);
        }
    }
}
