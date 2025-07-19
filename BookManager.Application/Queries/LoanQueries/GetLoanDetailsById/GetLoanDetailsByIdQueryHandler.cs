using BookManager.Application.Models;
using BookManager.Application.Queries.LoanQueries.GetLoanDetailsById;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Queries.LoanQueries.GetLoanDetailsById;

public class GetLoanDetailsByIdQueryHandler : IRequestHandler<GetLoanDetailsByIdQuery, ResultViewModel<GetLoanDetailsByIdQuery>>
{
    public GetLoanDetailsByIdQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    
    private readonly ILoanRepository _loanRepository;
    
    private const string LoanNotFoundMessage = "Loan Not Found.";
    
    public async Task<ResultViewModel<GetLoanDetailsByIdQuery>> Handle(GetLoanDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetDetailsById(request.Id);
        
        if (loan == null)
            return ResultViewModel<GetLoanDetailsByIdQuery>.Error(LoanNotFoundMessage);
        
        var model = GetLoanDetailsByIdQuery.FromEntity(loan);
        
        return ResultViewModel<GetLoanDetailsByIdQuery>.Success(model);
    }
}