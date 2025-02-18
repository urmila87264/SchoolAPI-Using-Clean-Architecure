using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    public  class SQLHelper:ISQLHelper
    {
        private readonly string _connectionString;
        public SQLHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        //public static void Initialize(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public  DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(commandText, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                connection.Close();
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Flag");
                dt.Columns.Add("Message");

                DataRow dr = dt.NewRow();
                dr["Flag"] = "0";
                dr["Message"] = ex.Message;

                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        public  int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            int a = 0;
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(commandText, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                connection.Open();
                a = command.ExecuteNonQuery();
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            return a;
        }

        public  async Task<int> ExecuteNonQueryAsync(string commandText, params SqlParameter[] parameters)
        {
            int result = 0;
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(commandText, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                await connection.OpenAsync(); // Asynchronously open the connection
                result = await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            return result;
        }

        public async Task<DataSet> ExecuteDataSetAsync(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(commandText, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                using var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                await Task.Run(() => adapter.Fill(ds)); // Fill is not async; wrapping it in Task.Run
                return ds;
                //await connection.OpenAsync(); // Asynchronously open the connection
                //ds = await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
                //return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            return ds;
        }

        //private string GenerateJwtToken(string username)
        //{
        //    var jwtSettings = _connectionString.GetSection();
        //}
    }

 


}
