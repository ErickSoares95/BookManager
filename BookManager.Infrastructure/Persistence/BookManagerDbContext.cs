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
        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Book>(e =>
                {
                    e.HasKey(s => s.Id);
                });
            builder
                .Entity<User>(e =>
                {
                    e.HasKey(u => u.Id);
                });
            builder
                .Entity<UserBook>(e =>
                {
                    e.HasKey(us => us.Id);

                    e.HasOne(p => p.Book)
                        .WithMany(p => p.Loans)
                        .HasForeignKey(p => p.IdBook)
                        .OnDelete(DeleteBehavior.Restrict);
                    e.HasOne(p => p.User)
                        .WithMany(u => u.Loans)
                        .HasForeignKey (p => p.IdUser)
                        .OnDelete(DeleteBehavior.Restrict);
                });
            
            base.OnModelCreating(builder);
        }
}
