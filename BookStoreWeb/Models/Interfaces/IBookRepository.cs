using BookStoreWeb.Model.Entities;
using BookStoreWeb.Models.Entities;

namespace BookStoreWeb.Models.Interfaces
{
    public interface IBookRepository
    {
        Task<Operation> GetBooks();
        Task<Operation> GetBookById(int id);
        Task<Operation> EditBook(int id, Book book);
        Task<Operation> InsertBook(Book book);
        Task<Operation> DeleteBook(int id);
    }
}
