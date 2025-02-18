using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public int UserID { get; set; }
        public string RoleId { get; set; }
        //public bool IsSuccess { get; set; }
        //public string Message { get; set; }
    }
}
