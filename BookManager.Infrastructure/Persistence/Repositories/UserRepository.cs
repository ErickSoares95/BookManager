using BookManager.Core.Entities;
using BookManager.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(BookManagerDbContext context)
    {
        _context = context;
    }

    private readonly BookManagerDbContext _context;
    
    public async Task<List<User>> GetAll()
    {
        var users = await _context.Users
            .Where(u => !u.IsDeleted)
            .ToListAsync();
        return users;
    }

    public async Task<User?> GetDetailsById(int id)
    {
        var users = await _context.Users
            .Include(u => u.Loans)
            .SingleOrDefaultAsync(u => u.Id == id);
        return users;
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<User?> GetByEmailAndPassword(string email, string hash)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == hash);
    }

    public async Task<int> Add(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user.Id;  
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Users.AnyAsync(x => x.Id == id);
    }
}