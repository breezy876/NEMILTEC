using NEMILTEC.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public class TableContainerModel : ContainerModelDecorator
    {
        public bool ShowFilters { get; set; }
        public Dictionary<string, NameValueModel> Filters { get; set; }

        public bool HasFilters => !Filters.IsNullOrEmpty();

        public bool HasPropertyLists => !PropertyLists.IsNullOrEmpty();

        public Dictionary<string, IContainerModel> PropertyLists { get; set; }

        public TableContainerModel() : base()
        {
            ShowFilters = true;

            Filters = new Dictionary<string, NameValueModel>();
            PropertyLists = new Dictionary<string, IContainerModel>();

            ContainerType = Code.Enums.ContainerType.Table;
        }

        public TableContainerModel(IContainerModel container) : base(container)
        {

            ShowFilters = true;

            Filters = new Dictionary<string, NameValueModel>();
            PropertyLists = new Dictionary<string, IContainerModel>();

            ContainerType = Code.Enums.ContainerType.Table;
        }
    }
}
