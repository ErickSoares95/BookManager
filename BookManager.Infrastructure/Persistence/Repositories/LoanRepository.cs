using BookManager.Core.Entities;
using BookManager.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Infrastructure.Persistence.Repositories;

public class LoanRepository(BookManagerDbContext _context) : ILoanRepository
{
    public async Task<List<Loan>> GetAll()
    {
        var loans = await _context.Loans
            .Where(b => !b.IsDeleted)
            .ToListAsync();
        return loans;
    }
    
    public async Task<Loan?> GetDetailsById(int id)
    {
        var loan = await _context.Loans
            .Include(b => b.User)
            .Include(b => b.Book)
            .SingleOrDefaultAsync(b => b.Id == id);
        return loan;
    }
    
    public async Task<int> Add(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
        
        return loan.Id;
    }

    public async Task Update(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Loans.AnyAsync(x => x.Id == id);
    }
    
    public async Task<Loan?> GetById(int id)
    {
        return await _context.Loans.SingleOrDefaultAsync(l => l.Id == id);
    }
}