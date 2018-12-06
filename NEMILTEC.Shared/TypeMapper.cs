using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Shared.Classes
{

    public static class TypeMapper
    {

        private static Dictionary<Type, NEMILTEC.Shared.Enums.Data.DataType> _fromTypes = new Dictionary<Type, Shared.Enums.Data.DataType>()
            {
                {typeof(long), Shared.Enums.Data.DataType.Number},
                {typeof(short), Shared.Enums.Data.DataType.Number},
                {typeof(int), Shared.Enums.Data.DataType.Number},

                {typeof(long?), Shared.Enums.Data.DataType.Number},
                {typeof(short?), Shared.Enums.Data.DataType.Number},
                {typeof(int?), Shared.Enums.Data.DataType.Number},

                {typeof(decimal), Shared.Enums.Data.DataType.Decimal},
                {typeof(decimal?), Shared.Enums.Data.DataType.Decimal},

                {typeof(string), Shared.Enums.Data.DataType.String},

                {typeof(bool), Shared.Enums.Data.DataType.Boolean},
                {typeof(bool?), Shared.Enums.Data.DataType.Boolean},

                {typeof(byte[]), Shared.Enums.Data.DataType.Object},

                 {typeof(DataEntity), Shared.Enums.Data.DataType.Entity}
            };

        private static Dictionary<NEMILTEC.Shared.Enums.Data.DataType, Type> _toTypes = new Dictionary
    <NEMILTEC.Shared.Enums.Data.DataType, Type>()
        {
                {Shared.Enums.Data.DataType.Number, typeof(long)},
                {Shared.Enums.Data.DataType.Decimal, typeof(decimal)},
                {Shared.Enums.Data.DataType.String, typeof(string)},
                {Shared.Enums.Data.DataType.Boolean, typeof(bool)},
                {Shared.Enums.Data.DataType.Object, typeof(byte[])}
        };

        public static Shared.Enums.Data.DataType Convert(Type type)
        {
            return _fromTypes[type];
        }

        public static Type Convert(NEMILTEC.Shared.Enums.Data.DataType type)
        {
            return _toTypes[type];
        }

    }
}
