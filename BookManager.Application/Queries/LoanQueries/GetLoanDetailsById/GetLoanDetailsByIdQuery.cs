using BookManager.Application.Models;
using BookManager.Core.Entities;
using BookManager.Core.Enums;
using MediatR;

namespace BookManager.Application.Queries.LoanQueries.GetLoanDetailsById;

public class GetLoanDetailsByIdQuery : IRequest<ResultViewModel<GetLoanDetailsByIdQuery>>
{
    public GetLoanDetailsByIdQuery(int id, int userId, int bookId, LoanStateEnum status, DateTime returnDate)
    {
        Id = id;
        UserId = userId;
        BookId = bookId;
        State = status;
        ReturnDate = returnDate;
    }
    
    public GetLoanDetailsByIdQuery(int id)
    {
         Id  = id;
    }
    public int Id { get;  set; }
    public DateTime ReturnDate { get;  set; }
    public LoanStateEnum State { get;  set; }
    public int UserId { get;  set; }
    public int BookId { get;  set; }
    
    public static GetLoanDetailsByIdQuery FromEntity(Loan loan)
        => new(
            loan.Id,
            loan.UserId,
            loan.BookId,
            loan.Status,
            loan.ReturnDate
        );
}