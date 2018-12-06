namespace NEMILTEC.Interfaces.Service.Data.Database
{
    /// <summary>
    /// data access interface
    /// for use with all database CRUD operations
    /// 
    // author: chris brown
    // date created: 24/06/2015
    /// </summary>
    /// 
    public interface IDatabase
    {


        /// <summary>
        /// execute a database command
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        int ExecuteCommand(string command);

        object ExecuteScalar(string query);


    }

    public interface IDatabase<T> : IDatabase
    {

        /// <summary>
        /// execute query and store result
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        T ExecuteQuery(string query);

    }
}
