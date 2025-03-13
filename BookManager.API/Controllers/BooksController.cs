using BookManager.Application.Commands.BookCommands.DeleteBook;
using BookManager.Application.Commands.BookCommands.InsertBook;
using BookManager.Application.Commands.BookCommands.UpdateBook;
using BookManager.Application.Queries.BookQueries.GetAllBooks;
using BookManager.Application.Queries.BookQueries.GetBookDetailsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    [HttpPost]
    public async Task<IActionResult> Post(InsertBookCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        
    }
    [HttpGet]
    public async Task<IActionResult> GetBooks(string search = "")
    {
        var query = new GetAllBooksQuery();
        
        var results = await _mediator.Send(query);
        
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetBookDetailsByIdQuery(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(UpdateBookCommand command)
    {
        var id = (HttpContext.Request.RouteValues["id"] ?? 0);
        if ((id is null) || id == "0")
            return BadRequest("Id is required");
        
        var result = await _mediator.Send(command);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteBookCommand(id));
        
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return NoContent();
    }
}