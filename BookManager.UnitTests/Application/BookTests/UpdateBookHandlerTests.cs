using BookManager.Application.Commands.BookCommands.DeleteBook;
using BookManager.Application.Commands.BookCommands.UpdateBook;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using NSubstitute;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Application.BookTests;

public class UpdateBookHandlerTests
{
    private const int ID = 1;
    public const string BOOK_NOT_FOUND_MESSAGE = "Book Not Found";
    
    [Fact]
    public async Task BookExistis_Update_Success_NSubstitute()
    {
        //Arrange
        var book = FakeDataHelper.CreateBook();
        
        var repository = Substitute.For<IBookRepository>();
        repository.GetById(ID).Returns(Task.FromResult<Book?>(book));
        repository.Update(Arg.Any<Book>()).Returns(Task.CompletedTask);
        
        var handler = new UpdateBookCommandHandler(repository);

        var command = new UpdateBookCommand();
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        await repository.Received(ID).GetById(ID);
        await repository.Received(ID).Update(Arg.Any<Book>());
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task BookDoesNotExists_Update_Error_NSubstitute()
    {
        var repository = Substitute.For<IBookRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Book?)null));
        
        var handler = new UpdateBookCommandHandler(repository);

        var command = new UpdateBookCommand();
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteBookCommandHandler.BOOK_NOT_FOUND_MESSAGE, result.Message);
        
        await repository.Received(ID).GetById(Arg.Any<int>());
        await repository.DidNotReceive().Update(Arg.Any<Book>());
    }
}