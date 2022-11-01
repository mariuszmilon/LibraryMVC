namespace LibraryMVC.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
    }
}
