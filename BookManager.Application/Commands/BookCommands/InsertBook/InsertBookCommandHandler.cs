using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.InsertBook;

public class InsertBookCommandHandler : IRequestHandler<InsertBookCommand, ResultViewModel<int>>
{
    public InsertBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    private readonly IBookRepository _bookRepository;
    
    public async Task<ResultViewModel<int>> Handle(InsertBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.ToEntity();
        
        var id = await _bookRepository.Add(book);
        
        return ResultViewModel<int>.Success(id);
    }
}