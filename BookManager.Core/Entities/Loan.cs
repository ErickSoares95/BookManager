using BookManager.Core.Enums;

namespace BookManager.Core.Entities;

public class Loan : BaseEntity
{
    
    public const string INVALID_STATE_MESSAGE = "Loan is in invalid state";
    public Loan(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        
        ReturnDate = DateTime.Now.AddDays(7);
        Status = LoanStateEnum.Borrowed;
    }
    
    public DateTime ReturnDate { get; private set; }
    public LoanStateEnum Status { get; private set; }
    public int UserId { get; private set; }
    public int BookId { get; private set; }
    public Book Book { get; private set; }
    public User User { get; private set; }
    
    public void CompleteLoan()
    {
        if (Status != LoanStateEnum.Borrowed)
            throw new InvalidOperationException(INVALID_STATE_MESSAGE);

        Status = LoanStateEnum.Complete;
    }

    public void LoanUpdate(LoanStateEnum status)
    {
        Status = status;
    }
}