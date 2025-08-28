using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.RecoveryPassword.Validate;

public class PasswordRecoveyValidateCommand : IRequest<ResultViewModel>
{
    public PasswordRecoveyValidateCommand(string email, string code)
    {
        Email = email;
        Code = code;
    }

    public string Email { get; set; }
    public string Code { get; set; }
}