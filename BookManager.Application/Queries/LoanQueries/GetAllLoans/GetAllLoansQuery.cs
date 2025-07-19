using BookManager.Application.Models;
using BookManager.Core.Entities;
using BookManager.Core.Enums;
using MediatR;

namespace BookManager.Application.Queries.LoanQueries.GetAllLoans;

public class GetAllLoansQuery : IRequest<ResultViewModel<List<GetAllLoansQuery>>>
{
    public GetAllLoansQuery(int id, int userId, int bookId, LoanStateEnum status, DateTime returnDate)
    {
        Id = id;
        UserId = userId;
        BookId = bookId;
        State = status;
        ReturnDate = returnDate;
    }
    
    public GetAllLoansQuery() { }

    public int Id { get;  set; }
    public DateTime ReturnDate { get;  set; }
    public LoanStateEnum State { get;  set; }
    public int UserId { get;  set; }
    public int BookId { get;  set; }
    
    public static GetAllLoansQuery FromEntity(Loan loan)
        => new(
            loan.Id,
            loan.UserId,
            loan.BookId,
            loan.Status,
            loan.ReturnDate
        );
}