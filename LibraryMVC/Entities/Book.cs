namespace LibraryMVC.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Translation { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string IDNumber { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfPiecesAvailable { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
