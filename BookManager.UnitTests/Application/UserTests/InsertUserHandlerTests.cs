using BookManager.Application.Commands.UserCommands.InsertUser;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using NSubstitute;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Application.UserTests;

public class InsertUserHandlerTests
{
    private const int ID = 1;
    
    [Fact]
    public async Task InputDataAreOk_InsertUser_Success_NSubstitute()
    {
        //Arrange
        var repository = Substitute.For<IUserRepository>();
        repository.Add(Arg.Any<User>()).Returns(Task.FromResult(ID));

        var command = FakeDataHelper.CreateFakeInsertUserCommand();
        
        var handler = new InsertUserCommandHandler(repository);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        await repository.Received(ID).Add(Arg.Any<User>());
        
    }
}