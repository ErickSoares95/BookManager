using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Queries.UserQueries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<GetAllUsersQuery>>>
{
    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private readonly IUserRepository _userRepository;


    public async Task<ResultViewModel<List<GetAllUsersQuery>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAll();
        
        var model = users.Select(GetAllUsersQuery.FromEntity).ToList();

        return ResultViewModel<List<GetAllUsersQuery>>.Success(model);
    }
}