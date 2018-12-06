using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Shared.Classes.Serializers
{
    public static class JSONSerializer
    {

        private static T _Deserialize<T>(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, NullValueHandling = NullValueHandling.Ignore, StringEscapeHandling = StringEscapeHandling.Default });
        }

        private static IDictionary<NEMILTEC.Shared.Enums.Data.DataType, Func<string, object>> _jsonDeserializers = new Dictionary
    <NEMILTEC.Shared.Enums.Data.DataType, Func<string, object>>()
        {

            {NEMILTEC.Shared.Enums.Data.DataType.String, (x) => _Deserialize<string>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Number, (x) =>_Deserialize<long>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Decimal, (x) => _Deserialize<Decimal>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Boolean, (x) => _Deserialize<bool>(x)},

            {NEMILTEC.Shared.Enums.Data.DataType.List, (x) => _Deserialize<ArrayList>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.DataTable, (x) => _Deserialize<DataTable>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Entity, (x) => _Deserialize<DataEntity>(x)},

            {NEMILTEC.Shared.Enums.Data.DataType.Date, (x) => _Deserialize<DateTime>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.Time, (x) => _Deserialize<DateTime>(x)},
            {NEMILTEC.Shared.Enums.Data.DataType.DateTime, (x) => _Deserialize<DateTime>(x)},
        };

        public static object Deserialize(NEMILTEC.Shared.Enums.Data.DataType type, string json)
        {
            return _jsonDeserializers[type](json);
        }


        public static string Serialize(object data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        public static T Deserialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static object Deserialize(Type type, string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, type);
        }

    }
}
