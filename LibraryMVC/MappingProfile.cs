using AutoMapper;
using LibraryMVC.Entities;
using LibraryMVC.Models;

namespace LibraryMVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookDto, Book>();
            CreateMap<Book, BookDto>()
                .ForMember(a => a.CategoryName, c => c.MapFrom(q => q.Category.NameOfCategory));

            CreateMap<EditBookDto, Book>();
            CreateMap<Book, EditBookDto>();

            CreateMap<RegisterDto, User>()
                .ForMember(a => a.Address, c => c.MapFrom(s => new Address {  City = s.City, Street = s.Street, PostalCode = s.PostalCode, StreetNumber = s.StreetNumber }));

        }
    }
}
