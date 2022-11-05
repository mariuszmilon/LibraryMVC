using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMVC.Services
{
    public interface IBookService
    {
        List<BookDto> GetAll();
        void Add(AddBookDto dto);
        SelectList SelectListItem();
        BookDto GetBookDtoById(int id);
        EditBookDto GetBookDtoByIdEdit(int id);
        void Edit(EditBookDto dto);
        void Delete(int id);
        void Borrow(int id);
    }
}
