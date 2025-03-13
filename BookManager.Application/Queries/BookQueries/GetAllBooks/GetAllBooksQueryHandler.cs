using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Queries.BookQueries.GetAllBooks;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ResultViewModel<List<GetAllBooksQuery>>>
{
    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    private readonly IBookRepository _bookRepository;
    
    public async Task<ResultViewModel<List<GetAllBooksQuery>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAll();
        
        var model = books.Select(GetAllBooksQuery.FromEntity).ToList();
        
        return ResultViewModel<List<GetAllBooksQuery>>.Success(model);
    }
}