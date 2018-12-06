using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Service.Data.Enums;

namespace NEMILTEC.Service.Data.Objects.Commands
{

    /// <summary>
    /// an abstract data command interface
    /// allows CRUD commands to be executed against a data object- any representation of relational tables e.g DataSet
    /// also allows an SQL command to be constructed without SQL knowledge
    /// </summary>
    public abstract class ADataCommand : ISQLObject
    {

        public DataCommandType Type { get; set; }
        /// <summary>
        /// the table for update/delete/insert
        /// </summary>
        public string EntityName { get; set; }


        public abstract string ToSQL();
    }
}
