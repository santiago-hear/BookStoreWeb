using BookStoreWeb.Model.Entities;
using BookStoreWeb.Models.Entities;

namespace BookStoreWeb.Models.Interfaces
{
    public interface IAutorRepository
    {
        Task<Operation> GetAutors();
        Task<Operation> GetAutorById(int id);
        Task<Operation> EditAutor(int id, Autor autor);
        Task<Operation> InsertAutor(Autor autor);
        Task<Operation> DeleteAutor(int id);
    }
}
