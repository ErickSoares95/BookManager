namespace BookManager.Core.Entities;

public class LoanBook : BaseEntity
{
    public int IdUser { get; private set; }
    public int IdBook { get; private set; }
    public DateTime ReturnDate { get; private set; }
    
    public LoanBook(int idUser, int idBook)
    {
        IdUser = idUser;
        IdBook = idBook;
        ReturnDate = DateTime.Now.AddDays(7);
    }
}