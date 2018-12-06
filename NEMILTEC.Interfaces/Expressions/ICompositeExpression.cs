using System.Collections.Generic;

namespace NEMILTEC.Interfaces.Service.Data.Expressions
{
    public interface ICompositeExpression : IDataExpression
    {
        IEnumerable<IDataExpression> Expressions { get; set; }

        int UpdateValues(IDictionary<string, object> nameValuePairs);
    }
}
