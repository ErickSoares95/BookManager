using BookManager.Application.Models;
using BookManager.Core.Entities;
using MediatR;

namespace BookManager.Application.Queries.BookQueries.GetBookDetailsById;

public class GetBookDetailsByIdQuery : IRequest<ResultViewModel<GetBookDetailsByIdQuery>>
{
    public GetBookDetailsByIdQuery(int id, string title, string author, string isbn, int publicationYear)
    {
        Id = id;
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }
    
    public GetBookDetailsByIdQuery(int id)
    {
         Id  = id;
    }
    public int Id { get; set; }
    
    public string Title { get;  set; }
    
    public string Author { get;  set; }
    
    public string ISBN { get;  set; }
    
    public  int  PublicationYear { get;  set; }
    
    //TODO: Será incluido os imprestimos após próximas atualizações do projeto e dependendo da necessidade
    
    public static GetBookDetailsByIdQuery FromEntity(Book book)
        => new(
            book.Id,
            book.Title,
            book.Author,
            book.ISBN,
            book.PublicationYear
        );
}