using BookManager.Application.Models;
using BookManager.Core.Entities;
using MediatR;

namespace BookManager.Application.Queries.UserQueries.GetAllUsers;

public class GetAllUsersQuery : IRequest<ResultViewModel<List<GetAllUsersQuery>>>
{
    public GetAllUsersQuery(string fullName, string email, DateTime birthday)
    {
        FullName = fullName;
        Email = email;
        Birthday = birthday;
    }
    
    public GetAllUsersQuery(){}

    public string FullName { get; set; }
    
    public string Email { get; set; }
    
    public DateTime Birthday { get; set; }

    public static GetAllUsersQuery FromEntity(User user)
        => new(
            user.FullName,
            user.Email,
            user.BirthDate
        );
}