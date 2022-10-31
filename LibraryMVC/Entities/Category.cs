namespace LibraryMVC.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string NameOfCategory { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}
