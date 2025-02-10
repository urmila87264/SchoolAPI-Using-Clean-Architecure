using Domain;
using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasture
{
    public interface IUserRepository 
    { 
        Task<bool> AddUserAsync(Registration user);

    }
}
