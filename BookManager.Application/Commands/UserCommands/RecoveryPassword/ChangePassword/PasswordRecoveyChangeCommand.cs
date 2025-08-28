using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.RecoveryPassword.ChangePassword;

public class PasswordRecoveyChangeCommand : IRequest<ResultViewModel>
{
    public PasswordRecoveyChangeCommand(string email, string code, string newPassword)
    {
        Email = email;
        Code = code;
        NewPassword = newPassword;
    }

    public string Email { get; set; }
    public string Code { get; set; }
    public string NewPassword { get; set; }
}