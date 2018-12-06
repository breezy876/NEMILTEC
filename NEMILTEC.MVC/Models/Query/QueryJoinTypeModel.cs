using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Query
{
    [Serializable]
    [ProtoContract()]
    public class QueryJoinTypeModel : Model
    {

        public QueryJoinTypeModel()
        {
            Type = ModelType.QueryJoinType;
            Title = "Join";
        }


    }
}
