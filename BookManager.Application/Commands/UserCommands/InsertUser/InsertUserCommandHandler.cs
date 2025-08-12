using BookManager.Application.Models;
using BookManager.Core.Repository;
using BookManager.Infrastructure.Auth;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.InsertUser;

public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
{
    public InsertUserCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        request.Password = _authService.ComputeHash(request.Password);
        
        var user = request.ToEntity();
        
        var id = await _userRepository.Add(user);
        
        return ResultViewModel<int>.Success(id);
    }
}