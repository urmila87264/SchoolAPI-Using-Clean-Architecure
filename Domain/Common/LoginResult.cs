using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class LoginResult
    {
        
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            //public Login User { get; set; } // Optional: Include user details if login succeeds
       
    }
}
