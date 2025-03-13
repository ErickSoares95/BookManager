using BookManager.Application.Models;
using BookManager.Core.Entities;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.InsertBook;

public class InsertBookCommand : IRequest<ResultViewModel<int>>
{
    public string Title { get;  set; }
    
    public string Author { get;  set; }
    
    public string ISBN  { get;  set; }
    
    public  int  PublicationYear { get;  set; }
    
    public Book ToEntity()
        => new Book(Title, Author, ISBN, PublicationYear);
}