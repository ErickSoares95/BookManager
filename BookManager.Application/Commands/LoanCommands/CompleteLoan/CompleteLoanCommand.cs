using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.LoanCommands.CompleteLoan;

public class CompleteLoanCommand : IRequest<ResultViewModel>
{
    public CompleteLoanCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}