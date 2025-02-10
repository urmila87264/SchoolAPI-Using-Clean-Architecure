using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Interfaces
{
    internal interface IPasswordHasher
    {
        string GeneratePassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
