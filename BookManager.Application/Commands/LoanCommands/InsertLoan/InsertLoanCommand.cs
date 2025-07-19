using BookManager.Application.Models;
using BookManager.Core.Entities;
using MediatR;

namespace BookManager.Application.Commands.LoanCommands.InsertLoan;

public class InsertLoanCommand : IRequest<ResultViewModel<int>>
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    
    public Loan ToEntity()
        => new Loan(UserId, BookId);
}