using System.ComponentModel;

namespace LibraryMVC.Models
{
    public class RegisterDto
    {
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [DisplayName("Email address")]
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }


    }
}
