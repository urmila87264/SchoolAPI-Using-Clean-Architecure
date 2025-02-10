using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    public interface ISQLHelper
    {
        int ExecuteNonQuery(string commandText, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string commandText, params SqlParameter[] parameters);
        DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters);
        Task<DataSet> ExecuteDataSetAsync(string commandText, params SqlParameter[] parameters);
    }
}
