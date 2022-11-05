using System.ComponentModel;

namespace LibraryMVC.Models
{
    public class BorrowedBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        [DisplayName("Borrowed stop")]
        public DateOnly DateOnlyStop { get; set; }
        [DisplayName("Borrowed start")]
        public DateOnly DateOnlyStart { get; set; }
    }
}
