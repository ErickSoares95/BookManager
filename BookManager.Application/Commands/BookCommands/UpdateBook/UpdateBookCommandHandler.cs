using BookManager.Application.Models;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ResultViewModel>
{
    public UpdateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    private const string BookNotFoundMessage = "Book Not Found.";

    private readonly IBookRepository _bookRepository;
    
    public async Task<ResultViewModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(request.BookId);
        
        if (book is null)
            return ResultViewModel.Error(BookNotFoundMessage);
        
        book.Update(request.Title, request.Author, request.ISBN, request.PublicationYear);
        
        await _bookRepository.Update(book);
        
        return ResultViewModel.Success();
    }
}