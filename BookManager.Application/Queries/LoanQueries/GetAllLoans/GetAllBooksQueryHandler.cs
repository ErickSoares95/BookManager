using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Queries.LoanQueries.GetAllLoans;

public class GetLoanDetailsByIdQueryHandler : IRequestHandler<GetAllLoansQuery, ResultViewModel<List<GetAllLoansQuery>>>
{
    public GetLoanDetailsByIdQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    private readonly ILoanRepository _loanRepository;
    
    public async Task<ResultViewModel<List<GetAllLoansQuery>>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
    {
        var books = await _loanRepository.GetAll();
        
        var model = books.Select(GetAllLoansQuery.FromEntity).ToList();
        
        return ResultViewModel<List<GetAllLoansQuery>>.Success(model);
    }
}