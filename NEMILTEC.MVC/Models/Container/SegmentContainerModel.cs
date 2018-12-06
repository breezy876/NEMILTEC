using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Models.Container
{
    [Serializable]
    public class SegmentContainerModel : ContainerModel
    {
        public SegmentContainerModel(IModel model) : base(model)
        {
            ContainerType = Code.Enums.ContainerType.Segment;
            Children = new List<IModel>(new[] {model});
        }

        public SegmentContainerModel() : base()
        {
            ContainerType = Code.Enums.ContainerType.Segment;
        }
    }
}
