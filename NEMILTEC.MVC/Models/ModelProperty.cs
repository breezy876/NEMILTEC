using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Code.Enums;

namespace NEMILTEC.MVC.Models
{
    [Serializable()]
    public class ModelProperty
    {
        public bool IsEditable { get; set; }
        public bool IsVisible { get; set; }
        public bool IsData { get; set; }

        public bool HasChanged { get; set; }

        public NEMILTEC.Shared.Enums.Data.DataType Type { get; set; }

        public string Name { get; set; }

        public object Value { get; set; }

        public ModelProperty()
        {
            IsVisible = true;
        }

        public ModelProperty(ModelProperty prop)
        {
            Type = prop.Type;
            Name = prop.Name;
            Value = prop.Value;
            IsEditable = prop.IsEditable;
            IsVisible = prop.IsVisible;
        }


        public void Update(ModelProperty prop)
        {
            Type = prop.Type;
            Name = prop.Name;
            Value = prop.Value;
            IsEditable = prop.IsEditable;
            IsVisible = prop.IsVisible;
        }

    }

}
