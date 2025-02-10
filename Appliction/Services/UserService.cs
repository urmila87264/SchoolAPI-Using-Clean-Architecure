using Appliction.Interfaces;
using Domain.Authentication;
using Infrasture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
       
        public UserService(IUserRepository userRepository)
        {

            _userRepository = userRepository;
           
        }
        public async Task<bool> SignUpAsync(Registration user)
        {

            return await _userRepository.AddUserAsync(user);
        }

       
    }
}
