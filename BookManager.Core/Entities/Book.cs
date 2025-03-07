using BookManager.Core.Enums;

namespace BookManager.Core.Entities;

public class Book : BaseEntity
{
    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        
        Status = BookStateEnum.Created;
        Loans = [];
    }
    
    public Book(){}

    public string Title { get; private set; }
    
    public string Author { get; private set; }
    
    public string ISBN  { get; private set; }
    
    public  int  PublicationYear { get; private set; }
    
    public BookStateEnum Status { get; private set; }
    public List<UserBook> Loans { get; private set; }
}

