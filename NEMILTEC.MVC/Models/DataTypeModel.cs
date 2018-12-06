using NEMILTEC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Shared.Classes.Serializers;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    public class DataTypeModel : Model
    {
        public DataTypeModel()
        {
            Name = "Data Type";
        }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<DataTypeModel>(copy);
        }

    }
}
