using BookManager.Application.Models;
using BookManager.Core.Enums;
using MediatR;

namespace BookManager.Application.Commands.LoanCommands.UpdateLoan;

public class UpdateLoanCommand : IRequest<ResultViewModel>
{
    public int LoanId;

    public LoanStateEnum Status;
}