using System.Collections;
using System.Collections.Generic;

namespace NEMILTEC.Interfaces.Service.Data.Objects
{
    public interface ISQLObject
    {

        string ToSQL(); //converts the data object into SQL
    }
}
