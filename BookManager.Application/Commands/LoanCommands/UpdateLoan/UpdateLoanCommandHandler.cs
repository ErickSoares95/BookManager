using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.LoanCommands.UpdateLoan;

public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, ResultViewModel>
{
    public UpdateLoanCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    
    public const string LOAN_NOT_FOUND_MESSAGE ="loan not found";
    
    private readonly ILoanRepository _loanRepository;

    

    public async Task<ResultViewModel> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetById(request.LoanId);

        if (loan is null)
            return ResultViewModel.Error(LOAN_NOT_FOUND_MESSAGE);
        
        loan.LoanUpdate(request.Status);
        
        await _loanRepository.Update(loan);
        
        return ResultViewModel.Success();
    }
}