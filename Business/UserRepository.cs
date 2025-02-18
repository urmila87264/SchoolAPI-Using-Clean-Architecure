using Appliction.Interfaces;
using AutoMapper.Configuration;
using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using DBHelper;
using Appliction.Procedure;
using Infrasture;

namespace Business
{
    public class UserRepository : IUserRepository
    {
        //private readonly string _connectionString;
        private readonly ISQLHelper _sqlHelper;
        //public UserRepository(string configuration) 
        //{ 
        //    _connectionString = configuration.GetConnectionString("DefaultCon");
        //}

        public UserRepository(ISQLHelper sQLHelper)
        {
            // _connectionString = connectionString;
            _sqlHelper = sQLHelper;

        }

        public async Task<bool> AddUserAsync(Registration user)
        {
            SqlParameter[] para = {
            new SqlParameter("@RoleID",user.RoleId),
            new SqlParameter("@Email",user.email),
            new SqlParameter("@Password",user.Password),
            new SqlParameter("@Name",user.name),
            new SqlParameter("@MotherName",user.motherName),
            new SqlParameter("@FatherName",user.fatherName),
            new SqlParameter("@Gender",user.gender),
            new SqlParameter("@DOB",user.DOB),
              new SqlParameter("@IsSuccess", SqlDbType.Bit) { Direction = ParameterDirection.Output },
                new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output }
            };

            int result = await _sqlHelper.ExecuteNonQueryAsync(SecurityProcedure.SignUp, para);
            bool isSuccess = Convert.ToBoolean(para[para.Length - 2].Value);
            string message = para[para.Length - 1].Value?.ToString() ?? "Unknown error occurred";

            return isSuccess;
            //return result > 0;
        }

        //    public async Task<bool> AddUserAsync(Registration user)
        //    {
        //        // string Password = PasswordHasher.GeneratePassword(user.Password);
        //        SqlParameter[] parameters = {
        //    new SqlParameter("@Username", user.Email),
        //    new SqlParameter("@RoleID", user.RoleId),
        //    //new SqlParameter("@Password", user.Password), // Hash before sending if possible
        //    //new SqlParameter("@Email", user.Email),
        //    //new SqlParameter("@Role", user.Role ?? (object)DBNull.Value),
        //    //new SqlParameter("@MotherName", user.MotherName ?? (object)DBNull.Value),
        //    //new SqlParameter("@FatherName", user.FatherName ?? (object)DBNull.Value),
        //    //new SqlParameter("@Gender", user.Gender ?? (object)DBNull.Value),
        //    //new SqlParameter("@StateID", user.StateId > 0 ? user.StateId : (object)DBNull.Value),
        //    //new SqlParameter("@CityID", user.CityId > 0 ? user.CityId : (object)DBNull.Value),
        //    //new SqlParameter("@Pincode", string.IsNullOrEmpty(user.Pincode) ? (object)DBNull.Value : user.Pincode),
        //    //new SqlParameter("@DOB", user.DOB ?? (object)DBNull.Value),
        //    //new SqlParameter("@Address", user.Address ?? (object)DBNull.Value),



        //   // new SqlParameter("@RoleID", user.RoleId >0? user.RoleId: (object)DBNull.Value),
        //    //new SqlParameter("@CourseID", user.CourseId > 0 ? user.CourseId : (object)DBNull.Value),
        //    //new SqlParameter("@IsSuccess", SqlDbType.Bit) { Direction = ParameterDirection.Output },
        //    //new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output }
        //};

        //        int result = await Task.Run(() => _sqlHelper.ExecuteNonQuery(SecurityProcedure.SignUp, parameters));

        //        // Use explicit indexes to avoid errors if parameter order changes
        //        //bool isSuccess = Convert.ToBoolean(parameters[parameters.Length - 2].Value);
        //        //string message = parameters[parameters.Length - 1].Value?.ToString() ?? "Unknown error occurred";

        //        //if (!isSuccess)
        //        //{
        //        //    Console.WriteLine($"User registration failed: {message}");
        //        //}

        //        //return isSuccess;
        //        return result > 0;
        //        //int result = await Task.Run(() => SQLHelper.ExecuteNonQuery(SecurityProcedure.SignUp, para));
        //    }


    }
}
