using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication
{
    public class Registration
    {
        public string email { get; set; }
        public string name { get; set; }
        public string Password { get; set; }
        public string confirmPassword { get; set; }
        public int RoleId { get; set; }
        public string motherName { get; set; }
        public string fatherName { get; set; }
        public string gender { get; set; }
        public string DOB { get; set; }
    }
}
