using System;
using NEMILTEC.Shared.Classes.Data;
using System.Data.SqlClient;
using System.Linq;
using NEMILTEC.Interfaces.Service.Data.Database;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Database
{

    /// <summary>
    /// DataTable data access interface
    /// for use with all database CRUD operations
    /// 
    // author: chris brown
    // date created: 24/06/2015
    /// </summary>
    public class Database : IDatabase<DataTable>
    {

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        private SqlConnection _connection;
        private SqlCommand _command;

        private  void _Initialize(string str)
        {
           _connection = new SqlConnection(ConnectionString);

            if (!DatabaseName.IsNullOrEmpty()) { _connection.ChangeDatabase(DatabaseName); }

            _command = new SqlCommand(str, _connection);
            _connection.Open();
        }

        public int ExecuteCommand(string command)
        {
            _Initialize(command);

            int totalRows = _command.ExecuteNonQuery();

            return totalRows;
        }

        public object ExecuteScalar(string query)
        {
            _Initialize(query);

            var retVal = _command.ExecuteScalar();

            return retVal;

        }

        public DataTable ExecuteQuery(string query)
        {

            var dt = new System.Data.DataTable();

            _Initialize(query);

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(_command);
            // this will query your database and return the result to your datatable
            da.Fill(dt);
            _connection.Close();
            da.Dispose();

            return new DataTable()
            {
                Columns = dt.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName).ToArray(),
                Rows = dt.Rows.Cast<System.Data.DataRow>().Select(r => new DataRow() { Values = r.ItemArray.Select(i => i.ToString()).ToArray() }).ToArray()
            };
        }

    }
}
