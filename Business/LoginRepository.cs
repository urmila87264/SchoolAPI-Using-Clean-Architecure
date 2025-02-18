using Appliction.Interfaces;
using Appliction.Procedure;
using DBHelper;
using Domain.Authentication;
using Domain.Common;
using System.Data;
using System.Data.SqlClient;

public class LoginRepository : ILoginRepository
{
    private readonly ISQLHelper _sqlHelper;

    public LoginRepository(ISQLHelper sqlHelper)
    {
        _sqlHelper = sqlHelper;
    }

    public async Task<Login> GetUserByUsernameAsync(string username, string password)
    {
        LoginResult res = new LoginResult();

        try
        {
            SqlParameter[] parameters = {
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password),
            new SqlParameter("@IsSuccess", SqlDbType.Bit) { Direction = ParameterDirection.Output },
            new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output },
            new SqlParameter("@UserID", SqlDbType.Int) { Direction = ParameterDirection.Output },
            new SqlParameter("@RoleId", SqlDbType.NVarChar, 20) { Direction = ParameterDirection.Output }
        };

            // Execute stored procedure asynchronously
            int result = await _sqlHelper.ExecuteNonQueryAsync(SecurityProcedure.Login, parameters);

            // Retrieve output parameters
            res.IsSuccess = parameters[2].Value != DBNull.Value && Convert.ToBoolean(parameters[2].Value);
            res.Message = parameters[3].Value?.ToString() ?? string.Empty;

            int userId = parameters[4].Value != DBNull.Value ? Convert.ToInt32(parameters[4].Value) : 0;
            string role = parameters[5].Value?.ToString();

            if (res.IsSuccess)
            {
                return new Login
                {
                    Email = username,
                    UserID = userId,
                    RoleId = role,
                    Password = password
                };//
            }
            else
            {
                Console.WriteLine($"Login failed: {res.Message}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetUserByUsernameAsync: {ex.Message}");
            return null;
        }
    }

    
}
