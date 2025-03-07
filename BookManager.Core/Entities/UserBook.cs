namespace BookManager.Core.Entities;

public class UserBook : BaseEntity
{
    public UserBook(int idUser, int idBook)
    {
        IdUser = idUser;
        IdBook = idBook;
        ReturnDate = DateTime.Now.AddDays(7);
    }
    
    public DateTime ReturnDate { get; private set; }
    public int IdUser { get; private set; }
    public int IdBook { get; private set; }
    public Book Book { get; private set; }
    public User User { get; private set; }
}