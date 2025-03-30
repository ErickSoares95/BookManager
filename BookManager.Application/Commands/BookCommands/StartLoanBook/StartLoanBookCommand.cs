using BookManager.Application.Models;
using MediatR;

namespace BookManager.Application.Commands.BookCommands.InsertLoanBook;

public class StartLoanBookCommand : IRequest<ResultViewModel>
{
    public DateTime ReturnDate { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
}