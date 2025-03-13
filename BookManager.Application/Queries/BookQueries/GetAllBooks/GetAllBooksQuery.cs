using BookManager.Application.Models;
using BookManager.Core.Entities;
using MediatR;

namespace BookManager.Application.Queries.BookQueries.GetAllBooks;

public class GetAllBooksQuery : IRequest<ResultViewModel<List<GetAllBooksQuery>>>
{
    public GetAllBooksQuery(int id, string title, string author, int publicationYear)
    {
        Id = id;
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
    }
    
    public GetAllBooksQuery() { }

    public int Id { get; set; }
    
    public string Title { get;  set; }
    
    public string Author { get;  set; }
    
    public  int  PublicationYear { get;  set; }
    
    public static GetAllBooksQuery FromEntity(Book book)
        => new(
            book.Id,
            book.Title,
            book.Author,
            book.PublicationYear
            );
}