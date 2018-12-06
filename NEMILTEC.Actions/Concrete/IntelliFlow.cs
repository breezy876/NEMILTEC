using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Abstract;

namespace NEMILTEC.Service.Automation.Concrete
{
    public class IntelliFlow : IIntelliFlow
    {

        public SharedIntelliFlowProperties Properties { get; set; }

        public IIntelliFlow Parent { get; set; }
        public IIntelliFlowIterator<IIntelliFlow> Children { get; set; }

        public IIntelliFlowIterator<IIntelliFlowItem> Items { get; set; }

        public IntelliFlowOutput Execute()
        {
            Properties = new SharedIntelliFlowProperties();
            return new IntelliFlowItemProcessor().Execute(this);
        }
    }

}
