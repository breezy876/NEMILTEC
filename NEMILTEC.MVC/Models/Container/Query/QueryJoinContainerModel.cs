using System;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Container.Query
{
    [Serializable]
    [ProtoContract]
    public class QueryJoinContainerModel : ContainerModel
    {
        public QueryJoinContainerModel() : base(new QueryJoinModel())
        {

        }


        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<QueryJoinContainerModel>(copy);
        }

    }
}
