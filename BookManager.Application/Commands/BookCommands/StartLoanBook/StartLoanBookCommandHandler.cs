using BookManager.Application.Models;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.InsertLoanBook;

public class StartLoanBookCommandHandler :IRequestHandler<StartLoanBookCommand, ResultViewModel>
{
    public StartLoanBookCommandHandler(IBookRepository bookRepository, IUserRepository userRepository)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }
    
    private const string BookMessageNotFound = "Book not found";
    private const string UserMessageNotFound = "User not found";
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    
    public async Task<ResultViewModel> Handle(StartLoanBookCommand request, CancellationToken cancellationToken)
    {
        if (!await _bookRepository.Exists(request.BookId))
            return ResultViewModel.Error(BookMessageNotFound);
        
        if (!await _userRepository.Exists(request.UserId))
            return ResultViewModel.Error(UserMessageNotFound);
        
        var book = await _bookRepository.GetById(request.BookId);
        
        book.StartLoanBook();
        
        await _bookRepository.Update(book);

        var loanBook = new LoanBook(request.UserId, request.BookId);
        
        await _bookRepository.AddLoan(loanBook);

        return ResultViewModel.Success();

    }
}