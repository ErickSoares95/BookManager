using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommand : IRequest<ResultViewModel>
{
    public int UserId { get; set; }
    public string FullName { get;  set; }
    public string Email { get;  set; }
    public DateTime BirthDate { get;  set; }
}