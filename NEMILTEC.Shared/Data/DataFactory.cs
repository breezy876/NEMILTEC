using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.Shared.Classes.Data
{
    public static class DataFactory
    {

        private static IDictionary<NEMILTEC.Shared.Enums.Data.DataType, object> _dataMap = new Dictionary
<NEMILTEC.Shared.Enums.Data.DataType, object>()
        {
            {NEMILTEC.Shared.Enums.Data.DataType.List, new ArrayList()},
            {NEMILTEC.Shared.Enums.Data.DataType.DataTable, new DataTable()},
            {NEMILTEC.Shared.Enums.Data.DataType.Entity, new DataEntity()},
        };

        public static object Create(NEMILTEC.Shared.Enums.Data.DataType type)
        {
            return _dataMap[type];
        }
    }
}
