using NEMILTEC.Service.Reporting.Concrete.Elements.TemplateInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Reporting.Enums;
using NEMILTEC.Shared.Classes.Serializers;

namespace NEMILTEC.Service.Reporting.Concrete
{
    public static class ReportElementTemplateInfoSerializer
    {

        private static IDictionary<ReportElementType, Func<byte[], object>> _deserializers = new Dictionary
            <ReportElementType, Func<byte[], object>>()
        {
            {ReportElementType.Chart, (x) => BinarySerializer.Deserialize<ChartElementTemplateInfo>(x)},
            {ReportElementType.Table, (x) => BinarySerializer.Deserialize<TableElementTemplateInfo>(x)},
            {ReportElementType.Text, (x) => BinarySerializer.Deserialize<TextElementTemplateInfo>(x)},
        };

        public static object Deserialize(ReportElementType type, byte[] data)
        {
            return _deserializers[type](data);
        }
    }
}
