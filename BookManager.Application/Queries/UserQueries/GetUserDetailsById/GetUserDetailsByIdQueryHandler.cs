using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Queries.UserQueries.GetUserDetailsById;

public class GetUserDetailsByIdQueryHandler : IRequestHandler<GetUserDetailsByIdQuery, ResultViewModel<GetUserDetailsByIdQuery>>
{
    public GetUserDetailsByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private const string UserNotFoundMessage = "Book Not Found.";
    private readonly IUserRepository _userRepository;
    public async Task<ResultViewModel<GetUserDetailsByIdQuery>> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);
        
        if (user is null)
            return ResultViewModel<GetUserDetailsByIdQuery>.Error(UserNotFoundMessage);
        
        var model = GetUserDetailsByIdQuery.FromEntity(user);
        
        return ResultViewModel<GetUserDetailsByIdQuery>.Success(model);
    }
}