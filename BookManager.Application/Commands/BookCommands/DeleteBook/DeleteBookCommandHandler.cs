using BookManager.Application.Models;
using BookManager.Core.Repository;
using BookManager.Infrastructure.Persistence.Repositories;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ResultViewModel>
{
    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _BookRepository = bookRepository;
    }

    private const string BookNotFoundMessage = "Book Not Found.";
    private readonly IBookRepository _BookRepository;
    
    public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _BookRepository.GetById(request.Id);

        if (book is null)
        {
            return ResultViewModel.Error(BookNotFoundMessage);
        }
        
        book.SetAsDeleted();
        
        await _BookRepository.Update(book);
        
        return ResultViewModel.Success();
    }
}