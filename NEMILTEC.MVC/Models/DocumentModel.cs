using NEMILTEC.Domain;
using NEMILTEC.MVC.Models.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    [ProtoContract]
    public class DocumentModel : Model
    {
        public DocumentModel() : base()
        {
            Type = Code.Enums.ModelType.Document;

            Name = "Document";


        }

        [ProtoMember(1)]
        public string FileName { get; set; }

        [ProtoMember(2)]
        public string Description { get; set; }

        [ProtoMember(3)]
        public byte[] Data { get; set; }

        [ProtoMember(4)]
        public long? FileSize { get; set; }

        [ProtoMember(5)]
        public long FileTypeId { get; set; }

        [ProtoMember(6)]
        public FileTypeModel FileType { get; set; }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<DocumentModel>(copy);
        }


    }
}
