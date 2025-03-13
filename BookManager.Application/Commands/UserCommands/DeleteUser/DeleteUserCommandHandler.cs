using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultViewModel>
{
    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private const string UserNotFoundMessage = "User not found";

    private readonly IUserRepository _userRepository;
    
    public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user == null)
            return ResultViewModel.Error(UserNotFoundMessage);
        
        user.SetAsDeleted();
        
        await _userRepository.Update(user);
        
        return ResultViewModel.Success();
    }
}