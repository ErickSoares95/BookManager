using BookManager.Core.Entities;
using BookManager.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Infrastructure.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    public BookRepository(BookManagerDbContext dbContext)
    {
        _context = dbContext;
    }
    
    private readonly BookManagerDbContext _context;
    
    public async Task<List<Book>> GetAll()
    {
        var books = await _context.Books
            .Where(b => !b.IsDeleted)
            .ToListAsync();
        return books;
    }

    public async Task<Book?> GetDetailsById(int id)
    {
        var books = await _context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == id);
        return books;
    }

    public async Task<Book?> GetById(int id)
    {
        return await _context.Books.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> Add(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        
        return book.Id;   
    }

    public async Task Update(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task AddLoan(LoanBook loanBook)
    {
        await _context.LoanBooks.AddAsync(loanBook);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Books.AnyAsync(x => x.Id == id);
    }
}