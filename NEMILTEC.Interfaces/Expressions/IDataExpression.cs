using System.Collections.Generic;

namespace NEMILTEC.Interfaces.Service.Data.Expressions
{


    public interface IDataExpression
    {

        object Evaluate(object data = null, IEnumerable<object> parameters = null);
    }
}
