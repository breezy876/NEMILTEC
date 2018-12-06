using System;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models.Container.Query
{
    [Serializable]
    [ProtoContract]
    public class QueryContainerModel : ContainerModel
    {
        public QueryContainerModel() : base(new QueryModel())
        {
   
        }

        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<QueryContainerModel>(copy);
        }

    }
}
