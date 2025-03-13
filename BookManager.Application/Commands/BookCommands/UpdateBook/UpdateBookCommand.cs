using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.UpdateBook;

public class UpdateBookCommand : IRequest<ResultViewModel>
{
    public int BookId { get; set; }
    
    public string Title { get;  set; }
    
    public string Author { get;  set; }
    
    public string ISBN  { get;  set; }
    
    public  int  PublicationYear { get;  set; }
}