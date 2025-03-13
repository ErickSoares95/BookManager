using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Queries.BookQueries.GetBookDetailsById;

public class GetProjectDetailsByIdQueryHandler : IRequestHandler<GetBookDetailsByIdQuery, ResultViewModel<GetBookDetailsByIdQuery>>
{
    public GetProjectDetailsByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    private const string BookNotFoundMessage = "Book Not Found.";

    private readonly IBookRepository _bookRepository;

    public async Task<ResultViewModel<GetBookDetailsByIdQuery>> Handle(GetBookDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetDetailsById(request.Id);

        if (book is null)
            return ResultViewModel<GetBookDetailsByIdQuery>.Error(BookNotFoundMessage);
        

        var model = GetBookDetailsByIdQuery.FromEntity(book);
        
        return ResultViewModel<GetBookDetailsByIdQuery>.Success(model);
    }
}