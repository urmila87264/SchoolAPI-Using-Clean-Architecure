using Appliction.Interfaces;
using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        //private readonly IPasswordHasher _passwordHasher;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            //_passwordHasher = passwordHasher;
        }

        public async Task<Login> GetUserByUsernameAsync(string username, string password)
        {
            return await _loginRepository.GetUserByUsernameAsync(username, password);

        }

        //public async Task<Login> LoginUser(Login user)
        //{
            
        //}
    }
}
