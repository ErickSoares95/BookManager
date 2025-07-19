using BookManager.Application.Commands.BookCommands.DeleteBook;
using BookManager.Application.Commands.BookCommands.InsertBook;
using BookManager.Application.Commands.LoanCommands.InsertLoan;
using BookManager.Application.Commands.BookCommands.UpdateBook;
using BookManager.Application.Commands.LoanCommands.CompleteLoan;
using BookManager.Application.Commands.LoanCommands.InsertLoan;
using BookManager.Application.Queries.BookQueries.GetAllBooks;
using BookManager.Application.Queries.BookQueries.GetBookDetailsById;
using BookManager.Application.Queries.LoanQueries.GetLoanDetailsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.API.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansController : ControllerBase
{
    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    [HttpPost]
    public async Task<IActionResult> Post(InsertLoanCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetLoanDetailsByIdQuery(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return Ok(result);
    }
    
    [HttpPut("{id}/complete")]
    public async Task<IActionResult>  CompleteLoanBook(CompleteLoanCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
}