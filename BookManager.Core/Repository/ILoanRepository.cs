using BookManager.Core.Entities;

namespace BookManager.Core.Repository;

public interface ILoanRepository
{
    Task<List<Loan>> GetAll();
    Task<Loan?> GetById(int id);
    Task<Loan?> GetDetailsById(int id);
    Task<int> Add(Loan loan);
    Task Update(Loan book);
    Task<bool> Exists(int id);
}