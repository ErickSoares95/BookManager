using BookManager.Application.Models;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using MediatR;

namespace BookManager.Application.Commands.LoanCommands.InsertLoan;

public class InsertLoanCommandHandler :IRequestHandler<InsertLoanCommand, ResultViewModel<int>>
{
    public InsertLoanCommandHandler(IBookRepository bookRepository, IUserRepository userRepository, ILoanRepository loanRepository)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _loanRepository = loanRepository;
    }
    
    private const string BookMessageNotFound = "Book not found";
    private const string UserMessageNotFound = "User not found";
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;
    
    public async Task<ResultViewModel<int>> Handle(InsertLoanCommand request, CancellationToken cancellationToken)
    {
        if (!await _bookRepository.Exists(request.BookId))
            return ResultViewModel<int>.Error(BookMessageNotFound);
        
        if (!await _userRepository.Exists(request.UserId))
            return ResultViewModel<int>.Error(UserMessageNotFound);
        
        var book = await _bookRepository.GetById(request.BookId);
        
        book.StartLoan();
        
        await _bookRepository.Update(book);

        var loan = new Loan(request.UserId, request.BookId);
        
        var id = await _loanRepository.Add(loan);

        return ResultViewModel<int>.Success(id);

    }
}