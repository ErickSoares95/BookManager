using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommandhandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
{
    public UpdateUserCommandhandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    private const string UserNotFoundMessage = "User Not Found.";

    private readonly IUserRepository _userRepository;
    
    public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user is null)
            return ResultViewModel.Error(UserNotFoundMessage);
        
        
        user.Update(request.FullName, request.Email, request.BirthDate);
        
        await _userRepository.Update(user);

        return ResultViewModel.Success();
    }
}