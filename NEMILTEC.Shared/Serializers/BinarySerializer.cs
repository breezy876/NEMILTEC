using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NEMILTEC.Domain;
using NEMILTEC.Shared.Classes.Data;
using ProtoBuf;
using ProtoBuf.Meta;

namespace NEMILTEC.Shared.Classes.Serializers
{
    /// <summary>
    /// binary serializer
    /// uses Protobuf
    /// </summary>
    public static class BinarySerializer
    {


        private static byte[] _Serialize<T>(T data)
        {
            //var stream = new MemoryStream();
            //ProtoBuf.Serializer.Serialize<T>(stream, data);
            //return stream.ToArray();

            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, data);
            return ms.ToArray();
        }

        private static T _Deserialize<T>(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter bf = new BinaryFormatter();
            var obj = (T)bf.Deserialize(ms);
            return obj;
        }

        private static Dictionary<NEMILTEC.Shared.Enums.Data.DataType, Func<object, byte[]>> _binarySerializers = new Dictionary
<NEMILTEC.Shared.Enums.Data.DataType, Func<object, byte[]>>()
        {
            {NEMILTEC.Shared.Enums.Data.DataType.String, (x) => _Serialize<string>((string)x) },

            {NEMILTEC.Shared.Enums.Data.DataType.Number, (x) => _Serialize<long>((long)x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Decimal, (x) => _Serialize<Decimal>((Decimal)x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Boolean, (x) => _Serialize<bool>((bool)x)},

            {NEMILTEC.Shared.Enums.Data.DataType.List, (x) => _Serialize<ArrayList>((ArrayList)x)},
            {NEMILTEC.Shared.Enums.Data.DataType.DataTable, (x) =>  _Serialize<DataTable>((DataTable)x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Entity, (x) => _Serialize<DataEntity>((DataEntity)x)},

            {NEMILTEC.Shared.Enums.Data.DataType.Date, (x) => _Serialize<DateTime>((DateTime)x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Time, (x) => _Serialize<DateTime>((DateTime)x)},
            {NEMILTEC.Shared.Enums.Data.DataType.DateTime, (x) => _Serialize<DateTime>((DateTime)x)}
        };

        private static Dictionary<NEMILTEC.Shared.Enums.Data.DataType, Func<byte[], object>> _binaryDeserializers = new Dictionary
<NEMILTEC.Shared.Enums.Data.DataType, Func<byte[], object>>()
        {
            {NEMILTEC.Shared.Enums.Data.DataType.String, (x) => _Deserialize<string>(x) },

            {NEMILTEC.Shared.Enums.Data.DataType.Number, (x) => _Deserialize<long>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Decimal, (x) => _Deserialize<Decimal>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Boolean, (x) =>_Deserialize<bool>(x)},

            {NEMILTEC.Shared.Enums.Data.DataType.List, (x) => _Deserialize<ArrayList>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.DataTable, (x) =>  _Deserialize<DataTable>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Entity, (x) => _Deserialize<DataEntity>(x)},

            {NEMILTEC.Shared.Enums.Data.DataType.Date, (x) => _Deserialize<DateTime>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Time, (x) => _Deserialize<DateTime>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.DateTime, (x) => _Deserialize<DateTime>(x)}
        };

        public static byte[] Serialize<T>(T data)
        {
            return _Serialize<T>(data);
        }

        public static T Deserialize<T>(byte[] data, bool useProtoBuf = true)
        {
            return _Deserialize<T>(data);
        }

        public static byte[] Serialize(NEMILTEC.Shared.Enums.Data.DataType type, object data)
        {
            return _binarySerializers[type](data);
        }

        public static object Deserialize(NEMILTEC.Shared.Enums.Data.DataType type, byte[] data)
        {
            return _binaryDeserializers[type](data);
        }

    }
}
