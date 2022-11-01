using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models
{
    public class LoginDto
    {
        [Display(Name ="Email address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
