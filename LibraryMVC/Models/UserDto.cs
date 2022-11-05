using System.ComponentModel;

namespace LibraryMVC.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        [DisplayName("First name")]
        public string UserName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [DisplayName("Zip code")]
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [DisplayName("Home number")]
        public int StreetNumber { get; set; }
        public string Role { get; set; }

    }
}
