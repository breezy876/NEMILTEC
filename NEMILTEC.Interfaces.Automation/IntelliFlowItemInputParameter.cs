using NEMILTEC.Interfaces.Service.Data.Expressions;

namespace NEMILTEC.Interfaces.Service.Automation
{
    public class IntelliFlowItemInputParameter
    {
        public SharedIntelliFlowProperties Properties { get; set; }
        public object Value { get; set; }
        public IDataExpression Expression { get; set; }

        public IntelliFlowItemInputParameter()
        {
            Properties = new SharedIntelliFlowProperties();
        }
    }
}
