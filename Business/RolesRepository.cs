using Appliction.Interfaces.Student;
using Appliction.Procedure;
using DBHelper;
using Domain.Common;
using System.Data;
using System.Data.SqlClient;


namespace Infrasture
{
    public class RolesRepository:IRolesRepository
    {
        private readonly ISQLHelper _sqlHelper;
        public RolesRepository(ISQLHelper sQLHelper)
        {
            _sqlHelper = sQLHelper;
        }

        public async Task<List<Roles>> GetAllRolesAsync()
        {
            SqlParameter[] para = { };
            DataSet ds = await _sqlHelper.ExecuteDataSetAsync(StudentProcedure.Roles, para);
            List<Roles> roles = new List<Roles>();
            if(ds != null && ds.Tables[0].Rows.Count>0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    roles.Add(new Roles()
                    {
                        RoleID =Convert.ToInt32( dr["RoleID"]),
                        RoleName = dr["RoleName"].ToString()
                    });
                }
            }
            return roles;
        }
    }
}
