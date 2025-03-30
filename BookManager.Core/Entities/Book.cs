using BookManager.Core.Enums;

namespace BookManager.Core.Entities;

public class Book : BaseEntity
{
    public const string INVALID_STATE_MESSAGE = "Book is in invalid state";
    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        
        Status = BookStateEnum.Available;
        Loans = [];
    }
    
    public Book(){}

    public string Title { get; private set; }
    
    public string Author { get; private set; }
    
    public string ISBN  { get; private set; }
    
    public  int  PublicationYear { get; private set; }
    
    public BookStateEnum Status { get; private set; }
    public List<LoanBook> Loans { get; private set; }

    public void Update(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }

    public void StartLoanBook()
    {
        if (Status != BookStateEnum.Available)
            throw new InvalidOperationException(INVALID_STATE_MESSAGE);

        Status = BookStateEnum.Reserved;
    }
}

