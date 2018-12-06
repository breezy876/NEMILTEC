using System.Collections.Generic;

namespace NEMILTEC.Interfaces.Service.Data.Objects
{
    public interface IDataQuery
    {
        QueryResult Execute(IDictionary<string, object> parameters);
    }
}
