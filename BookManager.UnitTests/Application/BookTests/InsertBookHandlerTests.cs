using BookManager.Application.Commands.BookCommands.InsertBook;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using FluentAssertions;
using Moq;
using NSubstitute;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Application.BookTests;

public class InsertBookHandlerTests
{
    private const int ID = 1;
    
    [Fact]
    public async Task InputDataAreOk_Insert_Success_Moq()
    {
        //Arrange
        var repository = Mock.Of<IBookRepository>(r =>
            r.Add(It.IsAny<Book>()) == Task.FromResult(ID));
        
        var command = FakeDataHelper.CreateFakeInsertBookCommand();
        
        var handler = new InsertBookCommandHandler(repository);
            
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        
        //Assert with FluentAssertions
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().Be(ID);
        
        Mock.Get(repository).Verify(r => r.Add(It.IsAny<Book>()), Times.Once);
    }
    
    
    [Fact]
    public async Task InputDataAreOk_Insert_Success_NSubstitute()
    {
        //Arrange
        var repository = Substitute.For<IBookRepository>();
        repository.Add(Arg.Any<Book>()).Returns(Task.FromResult(ID));

        var command = FakeDataHelper.CreateFakeInsertBookCommand();
        
        var handler = new InsertBookCommandHandler(repository);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        await repository.Received(ID).Add(Arg.Any<Book>());
    }
}