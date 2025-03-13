using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.DeleteBook;

public class DeleteBookCommand : IRequest<ResultViewModel>
{
    public DeleteBookCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}