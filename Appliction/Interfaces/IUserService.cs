using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Interfaces
{
    public interface IUserService
    {
        Task<bool> SignUpAsync(Registration user);
    }
}
