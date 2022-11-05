using System.Data;

namespace LibraryMVC.Entities
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string UserName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
}
