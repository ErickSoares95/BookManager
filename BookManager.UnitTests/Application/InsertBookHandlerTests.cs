using BookManager.Application.Commands.BookCommands.InsertBook;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using NSubstitute;

namespace TestProject1BookManager.UnitTests.Application;

public class InsertBookHandlerTests
{
    private const int ID = 1;
    [Fact]
    public async Task InputDataAreOk_Insert_Success_NSubstitute()
    {
        //Arrange
        var repository = Substitute.For<IBookRepository>();
        repository.Add(Arg.Any<Book>()).Returns(Task.FromResult(ID));

        var command = new InsertBookCommand
        {
            Title = "Harry Potter",
            Author = "J.K Rowling",
            ISBN = "9788869183157",
            PublicationYear = 1997
        };
        
        var handler = new InsertBookCommandHandler(repository);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        await repository.Received(ID).Add(Arg.Any<Book>());
    }
}