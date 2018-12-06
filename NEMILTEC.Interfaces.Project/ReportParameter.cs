using NEMILTEC.Interfaces.Service.Data.Expressions;

namespace NEMILTEC.Interfaces.Service.Reporting
{
    public class ReportElementParameter
    {
        public string Name { get; set; }
        public IDataExpression Expression { get; set; }
        public object Value { get; set; }
        public Shared.Enums.Data.DataType DataType { get; set; }
    }
}
