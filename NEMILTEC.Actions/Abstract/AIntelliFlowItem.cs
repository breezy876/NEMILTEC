using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Concrete;

namespace NEMILTEC.Service.Automation.Abstract
{
    public abstract class AIntelliFlowItem : IIntelliFlowItem
    {

        public SharedIntelliFlowProperties Properties { get; set; }

        public IIntelliFlow IntelliFlow { get; set; }

        public IIntelliFlowItem Parent { get; set; }
        public IIntelliFlowIterator<IIntelliFlowItem> Children { get; set; }

        public virtual IntelliFlowItemInput Input { get; set; }
        public virtual IntelliFlowItemOutput Output { get; set; }

        public abstract IntelliFlowItemOutput Execute();

        protected AIntelliFlowItem()
        {
            Input = new IntelliFlowItemInput();
            Output = new IntelliFlowItemOutput();
            Properties = new SharedIntelliFlowProperties();
        }
    }
}
