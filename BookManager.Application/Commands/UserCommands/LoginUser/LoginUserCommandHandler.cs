using BookManager.Application.Models;
using BookManager.Core.Repository;
using BookManager.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookManager.Application.Commands.UserCommands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginViewCommand>>
{
    public LoginUserCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }
    private const string USER_NOT_FOUND = "Email or password is incorrect.";   
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    
    public async Task<ResultViewModel<LoginViewCommand>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAndPassword(request.Email, _authService.ComputeHash(request.Password));
        
        if (user is null)
            return ResultViewModel<LoginViewCommand>.Error(USER_NOT_FOUND);

        var token = _authService.GenerateToken(user.Email , user.Role);

        var viewModel = new LoginViewCommand(token);
        
        return ResultViewModel<LoginViewCommand>.Success(viewModel);
    }
}