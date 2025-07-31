using BookManager.Application.Commands.BookCommands.DeleteBook;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using FluentAssertions;
using Moq;
using NSubstitute;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Application.BookTests;

public class DeleteBookHandlerTests
{
    private const int ID = 1;
    public const string BOOK_NOT_FOUND_MESSAGE = "Book Not Found";
    [Fact]
    public async Task BookExists_Delete_Success_NSubstitute()
    {
        //Arrange
        var book = FakeDataHelper.CreateBook();
        
        var repository = Substitute.For<IBookRepository>();
        repository.GetById(ID).Returns(Task.FromResult<Book?>(book));
        repository.Update(Arg.Any<Book>()).Returns(Task.CompletedTask);
        
        var handler = new DeleteBookCommandHandler(repository);
        
        var command = new DeleteBookCommand(ID);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        await repository.Received(ID).GetById(ID);
        await repository.Received(ID).Update(Arg.Any<Book>());
        
        //Assert with FluentAssertions
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task BookDoesNotExist_Delete_Error_NSubstitute()
    {
        //Arrange
        var repository = Substitute.For<IBookRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Book?)null));
        
        var handler = new DeleteBookCommandHandler(repository);
        
        var command = new DeleteBookCommand(ID);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteBookCommandHandler.BOOK_NOT_FOUND_MESSAGE, result.Message);
        await repository.Received(ID).GetById(Arg.Any<int>());
        await repository.DidNotReceive().Update(Arg.Any<Book>());
        
        //Assert with FluentAssertions
        result.IsSuccess.Should().BeFalse();
        
    }
    
    [Fact]
    public async Task ProjectExists_Delete_Success_Moq()
    {
        //Arrange
        var book = FakeDataHelper.CreateBook();

        var repository = Mock.Of<IBookRepository>(b =>
            b.GetById(It.IsAny<int>()) == Task.FromResult(book) &&
            b.Update(It.IsAny<Book>()) == Task.CompletedTask
        );

        var handler = new DeleteBookCommandHandler(repository);
        
        var command = new DeleteBookCommand(ID);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        Mock.Get(repository).Verify(r => r.GetById(ID), Times.Once);
        Mock.Get(repository).Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
    }
    
    [Fact]
    public async Task BookDoesNotExist_Delete_Error_Moq()
    {
        //Arrange
        var repository = Mock.Of<IBookRepository>(r => 
            r.GetById(It.IsAny<int>()) == Task.FromResult((Book?) null)
        );
        
        var handler = new DeleteBookCommandHandler(repository);
        
        var command = new DeleteBookCommand(ID);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteBookCommandHandler.BOOK_NOT_FOUND_MESSAGE, result.Message);
        
        Mock.Get(repository).Verify(r => r.GetById(ID), Times.Once);
        Mock.Get(repository).Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
        
    }
}