using BookManager.Core.Entities;

namespace BookManager.Core.Repository;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User?> GetDetailsById(int id);
    Task<User?> GetById(int id);
    Task<int> Add(User book);
    Task Update(User book);
    Task<bool> Exists(int id);
}