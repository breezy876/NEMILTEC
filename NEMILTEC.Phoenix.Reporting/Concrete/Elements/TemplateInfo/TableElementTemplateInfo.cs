using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace NEMILTEC.Service.Reporting.Concrete.Elements.TemplateInfo
{
    [ProtoContract()]
    public class TableElementTemplateInfo
    {
        [ProtoMember(1)]
        public long Index { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
    }
}
