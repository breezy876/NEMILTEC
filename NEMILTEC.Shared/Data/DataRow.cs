using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace NEMILTEC.Shared.Classes.Data
{
    [Serializable]
    [ProtoContract()]
    public class DataRow
    {
        [ProtoMember(1)]
        public string[] Values { get; set; }
    }
}
