using NEMILTEC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    [ProtoContract]
    public class FileTypeModel : Model
    {
        public FileTypeModel()
        {
            Name = "File Type";
            Type = ModelType.FileType;
        }

        [ProtoMember(1)]
        public string Extension { get; set; }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<FileTypeModel>(copy);
        }

    }
}
