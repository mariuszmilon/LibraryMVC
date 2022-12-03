using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models
{
    public class BorrowedBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [DataType(DataType.Date)]
        public DateTime Stop { get; set; }
        [DisplayName("Borrowed stop")]
        [DataType(DataType.Date)]
        public DateOnly DateOnlyStop { get; set; }
        [DisplayName("Borrowed start")]
        [DataType(DataType.Date)]
        public DateOnly DateOnlyStart { get; set; }
    }
}
