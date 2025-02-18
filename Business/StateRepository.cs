using Appliction.Common.Response;
using Appliction.Interfaces.Common;
using Appliction.Procedure;
using DBHelper;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasture
{
    public class StateRepository : IStateRepositry
    {
        private readonly ISQLHelper _sqlHelper;
        public StateRepository(ISQLHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }
       

        public async Task<List<State>> GetAllStatesAsync()
        {
            SqlParameter[] para = { 
            new SqlParameter("@Operation","SELECT")
            };
            DataSet ds = await _sqlHelper.ExecuteDataSetAsync(CommonProcedure.State, para);
            List<State> states = new List<State>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    states.Add(new State()
                    {
                        StateID = Convert.ToInt32(dr["StateID"]),
                        stateName = dr["StateName"].ToString()
                        // CountryID= Convert.ToInt32(dr["RoleID"])
                    });
                }
            }
            return states;
        }

     

      
    }
}
