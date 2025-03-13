using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.InsertUser;

public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
{
    public InsertUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    

    private readonly IUserRepository _userRepository;
    public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();
        
        var id = await _userRepository.Add(user);
        
        return ResultViewModel<int>.Success(id);
    }
}