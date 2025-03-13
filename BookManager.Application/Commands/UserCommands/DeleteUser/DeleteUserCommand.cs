using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest<ResultViewModel>
{
    public DeleteUserCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}