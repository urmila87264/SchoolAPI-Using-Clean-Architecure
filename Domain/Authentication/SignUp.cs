using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Authentication
{
    public class SignUp
    {
        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        //public string UserName { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

       // [Required(ErrorMessage = "Password is required")]
       // [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required")]
        //[Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

       // [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        public string MotherName { get; set; }
        public string FatherName { get; set; }

       // [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

       // [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }

       // [Required(ErrorMessage = "City is required")]
        public int CityId { get; set; }

        //[Required(ErrorMessage = "Pincode is required")]
        //[RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be 6 digits")]
        public string Pincode { get; set; }

        //[Required(ErrorMessage = "Date of Birth is required")]
        //[DataType(DataType.Date)]
        public string DOB { get; set; }  // ✅ Use DateTime instead of string

       // [Required(ErrorMessage = "Address is required")]
        //[StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Role ID is required")]
        public string RoleID { get; set; }

        //[Required(ErrorMessage = "Course ID is required")]
        public int CourseId { get; set; }

        /// <summary>
        /// ✅ Advanced Validation Method
        /// </summary>
        //public bool IsValid()
        //{
        //    return ValidateEmail() && ValidatePassword();
        //}

        /// <summary>
        /// ✅ Ensure Email Format is Correct
        /// </summary>
        //private bool ValidateEmail()
        //{
        //    return !string.IsNullOrWhiteSpace(Email) && new EmailAddressAttribute().IsValid(Email);
        //}

        /// <summary>
        /// ✅ Ensure Strong Password
        /// </summary>
        //private bool ValidatePassword()
        //{
        //    if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
        //        return false;

        //    // ✅ Strong Password: At least 1 uppercase, 1 lowercase, 1 number, and 1 special character
        //    string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
        //    return Regex.IsMatch(Password, passwordPattern);
        //}
    }
}
