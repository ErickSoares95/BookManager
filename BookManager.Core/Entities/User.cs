namespace BookManager.Core.Entities;

public class User
{
    public User(string fullName, string email, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Active = true;
        
        
        ActiveLoans = [];
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool Active { get; private set; }
    public List<Book> ActiveLoans { get; private set; }
}