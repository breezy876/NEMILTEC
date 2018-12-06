using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Abstract;

namespace NEMILTEC.Service.Automation.Concrete.Rules
{
    public class IntelliFlowTrueFalseRule : AIntelliFlowRule
    {
        public override IntelliFlowItemOutput Execute()
        {
            return new IntelliFlowItemProcessor().Execute(this);
        }
    }
}
