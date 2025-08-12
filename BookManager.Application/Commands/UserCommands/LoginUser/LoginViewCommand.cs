namespace BookManager.Application.Commands.UserCommands.LoginUser;

public class LoginViewCommand
{
    public LoginViewCommand(string token)
    {
        Token = token;
    }
    public string Token { get; set; }
}