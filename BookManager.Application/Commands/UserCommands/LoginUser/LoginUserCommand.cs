using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.LoginUser;

public class LoginUserCommand :IRequest<ResultViewModel<LoginViewCommand>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}