using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.RecoveryPassword.Request;

public class PasswordRecoveyRequestCommand :IRequest<ResultViewModel>
{
    public PasswordRecoveyRequestCommand(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}