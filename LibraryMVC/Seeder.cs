using LibraryMVC.Controllers;
using LibraryMVC.Entities;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace LibraryMVC
{
    public class Seeder
    {
        private readonly LibraryDbContext _dbContext;
        public Seeder(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        private List<IdentityRole> GetRoles()
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "864cd614-adcd-46f1-9901-6317c8195d99",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },

                new IdentityRole()
                {
                    Id = "390bf487-f2a1-42e6-be69-295b7095ffec",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },

                new IdentityRole()
                {
                    Id = "5433032b-e50b-4e8e-91a6-bb68eab0bb85", 
                    Name = "User", 
                    NormalizedName = "USER", 
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }

            };

            return roles;
        }
    }
}
