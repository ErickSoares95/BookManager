using BookManager.Core.Entities;

namespace BookManager.Core.Repository;

public interface IBookRepository
{
    Task<List<Book>> GetAll();
    Task<Book?> GetDetailsById(int id);
    Task<Book?> GetById(int id);
    Task<int> Add(Book book);
    Task Update(Book book);
    Task AddLoan(UserBook userBook );
    Task<bool> Exists(int id);
}