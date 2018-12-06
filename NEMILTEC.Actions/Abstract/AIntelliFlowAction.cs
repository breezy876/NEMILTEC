using NEMILTEC.Interfaces.Service.Automation;

namespace NEMILTEC.Service.Automation.Abstract
{
    public abstract class AIntelliFlowAction : AIntelliFlowItem
    {
        public IntelliFlowActionState State { get; set; }



    }
}
