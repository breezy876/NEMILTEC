using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.MVC.Code.Enums;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Query
{
    [Serializable]
    [ProtoContract()]
    public abstract class QueryChildModel : Model, IQueryChild
    {
        protected QueryChildModel()
        {
            IsChild = true;
            ParentType = ModelType.Query;
        }
        [ProtoMember(1)]
        public long QueryId { get; set; }
    }
}
