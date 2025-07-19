using BookManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Infrastructure.Persistence;

public class BookManagerDbContext : DbContext
{
    public BookManagerDbContext(DbContextOptions<BookManagerDbContext> options)
        : base(options)
    {
        
    }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Book>(e =>
                {
                    e.HasKey(s => s.Id);
                    
                    e.HasMany(b => b.Loans)
                        .WithOne(l => l.Book)
                        .HasForeignKey(l => l.BookId)
                        .OnDelete(DeleteBehavior.Restrict);
                });
            builder
                .Entity<User>(e =>
                {
                    e.HasKey(u => u.Id);
                    
                    e.HasMany(u => u.Loans)
                        .WithOne(l => l.User)
                        .HasForeignKey(u => u.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                });
            builder
                .Entity<Loan>(e =>
                {
                    e.HasKey(l => l.Id);
                    
                    e.HasOne(l => l.Book)
                        .WithMany(b => b.Loans)
                        .HasForeignKey(p => p.BookId)
                        .OnDelete(DeleteBehavior.Restrict);
                    
                    e.HasOne(l => l.User)
                        .WithMany(u => u.Loans)
                        .HasForeignKey (p => p.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                });
            
            base.OnModelCreating(builder);
        }
}
