using BookManager.Application.Commands.UserCommands.DeleteUser;
using BookManager.Application.Commands.UserCommands.InsertUser;
using BookManager.Application.Commands.UserCommands.UpdateUser;
using BookManager.Application.Queries.UserQueries.GetAllUsers;
using BookManager.Application.Queries.UserQueries.GetUserDetailsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;
    
    [HttpPost]
    public async Task<IActionResult> Post(InsertUserCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers(string search = "")
    {
        var query = new GetAllUsersQuery();
        
        var results = await _mediator.Send(query);
        
        return Ok(results);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserDetailsByIdQuery(id));
    
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(UpdateUserCommand command)
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
        var result = await _mediator.Send(new DeleteUserCommand(id));
        
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return NoContent();
    }
}