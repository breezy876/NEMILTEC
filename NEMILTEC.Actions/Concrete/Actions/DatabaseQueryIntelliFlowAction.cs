using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Service.Automation.Abstract;

namespace NEMILTEC.Service.Automation.Concrete.Actions
{
    public class DatabaseQueryIntelliFlowAction : AIntelliFlowAction
    {
        public ISQLObject Query { get; set; }

        public override IntelliFlowItemOutput Execute()
        {
            return new IntelliFlowItemProcessor().Execute(this);
        }
    }
}
