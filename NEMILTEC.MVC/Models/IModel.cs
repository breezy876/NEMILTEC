using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Code.Enums;

namespace NEMILTEC.MVC.Models
{
    public interface IModel
    {
        //TODO dynamic styling

        long Id { get; set; }

        long Index { get; set; }

        ModelType Type { get; set; }

        bool IsNew { get; set; }
        bool IsVisible { get; set; }
        bool IsEditable { get; set; }
        bool IsContainer { get; set; }
        bool IsChild { get; set; }

        List<IModel> Children { get; set; }

        ModelType ParentType { get; set; }

        Dictionary<string, ModelProperty> Properties { get; set; }
        Dictionary<string, string> PropertyMappings { get; set; }

        List<ModelCommand> Commands { get; set; }

        string Title { get; set; }
        string Name { get; set; }
        string CategoryName { get; set; }


        /// <summary>
        /// pre build logic
        /// </summary>
        /// <param name="updateFunc"></param>
        void Initialize(Func<IModel, IModel> initFunc = null);

        /// <summary>
        /// post build logic
        /// </summary>
        /// <param name="updateFunc"></param>
        void Update(Func<IModel, IModel> updateFunc = null);

        void Clear();

        IModel Copy();
    }

}
