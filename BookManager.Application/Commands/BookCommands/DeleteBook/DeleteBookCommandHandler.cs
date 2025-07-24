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

    public const string BOOK_NOT_FOUND_MESSAGE = "Book Not Found.";
    private readonly IBookRepository _BookRepository;
    
    public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _BookRepository.GetById(request.Id);

        if (book is null)
        {
            return ResultViewModel.Error(BOOK_NOT_FOUND_MESSAGE);
        }
        
        book.SetAsDeleted();
        
        await _BookRepository.Update(book);
        
        return ResultViewModel.Success();
    }
}