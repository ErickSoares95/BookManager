using BookManager.Application.Models;
using BookManager.Core.Entities;
using BookManager.Core.Enums;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.InsertUser;

public class InsertUserCommand: IRequest<ResultViewModel<int>>
{
    public string FullName { get;  set; }
    public string Email { get;  set; }
    public DateTime BirthDate { get;  set; }
    public string Password { get;  set; }
    public Role Role { get;  set; }
    
    public User ToEntity()
        => new User(FullName, Email, BirthDate, Password, Role);
}