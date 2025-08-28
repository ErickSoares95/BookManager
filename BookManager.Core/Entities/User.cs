using BookManager.Core.Enums;

namespace BookManager.Core.Entities;

public class User : BaseEntity
{
    public User(string fullName, string email, DateTime birthDate, string password, string role)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Password = password;
        Role = role;
        
        Active = true;
        Loans = [];
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool Active { get; private set; }
    public string Password {get; private set;}
    public  string Role { get; private set; }
    public List<Loan> Loans { get; private set; }

    public void Update(string fullName, string email, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }
}