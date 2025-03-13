using BookManager.Application.Models;
using BookManager.Core.Entities;
using MediatR;

namespace BookManager.Application.Queries.UserQueries.GetUserDetailsById;

public class GetUserDetailsByIdQuery : IRequest<ResultViewModel<GetUserDetailsByIdQuery>>
{
    public GetUserDetailsByIdQuery(int id, string fullName, string email, DateTime birthday)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Birthday = birthday;
    }
    public GetUserDetailsByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    
    public string FullName { get; set; }
    
    public string Email { get; set; }
    
    public DateTime Birthday { get; set; }

    public static GetUserDetailsByIdQuery FromEntity(User user)
        => new(
            user.Id,
            user.FullName,
            user.Email,
            user.BirthDate
        );
}