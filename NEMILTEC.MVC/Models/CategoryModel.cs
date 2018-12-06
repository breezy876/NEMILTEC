using NEMILTEC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    [ProtoContract()]
    public class CategoryModel : Model
    {
        public CategoryModel()
        {
            Title = "Category";
            Type = ModelType.Category;
        }

        public override IModel Copy()
        {
            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<CategoryModel>(copy);
        }

    }
}
