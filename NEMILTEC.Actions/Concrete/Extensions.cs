using NEMILTEC.Interfaces.Service.Automation;

namespace NEMILTEC.Service.Automation.Concrete
{
    public static class Extensions
    {
        public static object GetValue(this IntelliFlowItemInputParameter parameter, object data)
        {
            return parameter.Expression != null ?
            parameter.Expression.Evaluate(data) :
            parameter.Value;
        }
    }
}
