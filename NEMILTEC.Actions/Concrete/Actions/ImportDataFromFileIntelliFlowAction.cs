using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Abstract;

namespace NEMILTEC.Service.Automation.Concrete.Actions
{
    public class ImportDataFromFileIntelliFlowAction : AIntelliFlowAction
    {

        public override IntelliFlowItemOutput Execute()
        {
            return new IntelliFlowItemProcessor().Execute(this);
        }
    }
}
