using BookManager.Core.Enums;

namespace BookManager.Core.Entities;

public class LoanBook : BaseEntity
{
    public LoanBook(int userId, int bookId)
    {
        ReturnDate = DateTime.Now.AddDays(7);
        UserId = userId;
        BookId = bookId;
    }
    
    public DateTime ReturnDate { get; private set; }
    public int UserId { get; private set; }
    public int BookId { get; private set; }
    public Book Book { get; private set; }
    public User User { get; private set; }
}