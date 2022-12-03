using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Models;

namespace LibraryMVC.Entities
{
    public class LibraryDbContext : IdentityDbContext<User>
    {
        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=LibraryMVCDb;Trusted_Connection=True";

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category() { Id = 1, NameOfCategory = "Action or Adventure" },
                    new Category() { Id = 2, NameOfCategory = "Classics" },
                    new Category() { Id = 3, NameOfCategory = "Comic Book or Graphic Novel" },
                    new Category() { Id = 4, NameOfCategory = "Detective and Mystery" },
                    new Category() { Id = 5, NameOfCategory = "Fantasy" },
                    new Category() { Id = 6, NameOfCategory = "Historical Fiction" },
                    new Category() { Id = 7, NameOfCategory = "Horror" },
                    new Category() { Id = 8, NameOfCategory = "Literary Fiction" },
                    new Category() { Id = 9, NameOfCategory = "Romance" },
                    new Category() { Id = 10, NameOfCategory = "Science Fiction" },
                    new Category() { Id = 11, NameOfCategory = "Suspense or Thrillers" },
                    new Category() { Id = 12, NameOfCategory = "Biographies or Autobiographies" },
                    new Category() { Id = 13, NameOfCategory = "Cookbooks" },
                    new Category() { Id = 14, NameOfCategory = "History" },
                    new Category() { Id = 15, NameOfCategory = "Memoir" },
                    new Category() { Id = 17, NameOfCategory = "Self-Help" },
                    new Category() { Id = 18, NameOfCategory = "True Crime" });

            modelBuilder.Entity<Book>()
                .HasOne(r => r.Category)
                .WithMany(u => u.Books)
                .HasForeignKey(r => r.CategoryId);

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


    }
}
