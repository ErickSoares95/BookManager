using BookManager.Application.Models;
using BookManager.Core.Enums;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.LoanCommands.CompleteLoan;

public class CompleteLoanCommandHandler : IRequestHandler<CompleteLoanCommand, ResultViewModel>
{
    public CompleteLoanCommandHandler(ILoanRepository loanRepository)
    {
        _LoanRepository = loanRepository;
    }
    
    private const string LoanNotFoundMessage = "Loan Not Found.";
    private readonly ILoanRepository _LoanRepository;
    
    
    public async Task<ResultViewModel> Handle(CompleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _LoanRepository.GetById(request.Id);

        if (loan is null)
            return ResultViewModel.Error(LoanNotFoundMessage);

        loan.CompleteLoan();
        
        await _LoanRepository.Update(loan);
        
        return ResultViewModel.Success();
    }
}