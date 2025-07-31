using BookManager.Application.Commands.UserCommands.DeleteUser;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using NSubstitute;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Application.UserTests;

public class DeleteUserHandlerTests
{
    private const int ID = 1;
    public const string USER_NOT_FOUND_MESSAGE = "User Not Found";

    [Fact]
    public async Task UserExists_Delete_success_Nsubstitute()
    {
        //Arrange
        var user = FakeDataHelper.CreateUser();
        
        var repository = Substitute.For<IUserRepository>();
        repository.GetById(ID).Returns(Task.FromResult<User?>(user));
        repository.Update(Arg.Any<User>()).Returns(Task.CompletedTask);

        var handler = new DeleteUserCommandHandler(repository);
        
        var command = new DeleteUserCommand(ID);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        await repository.Received(ID).GetById(ID);
        await repository.Received(ID).Update(Arg.Any<User>());
    }

    [Fact]
    public async Task UserDoesNotExist_Delete_Error_NSubstitute()
    {
        //Arrange
        var repository = Substitute.For<IUserRepository>();
        repository.GetById(ID).Returns(Task.FromResult<User?>(null));
        
        var handler = new DeleteUserCommandHandler(repository);
        
        var command = new DeleteUserCommand(ID);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteUserCommandHandler.USER_NOT_FOUND_MESSAGE, result.Message);
        await repository.Received(ID).GetById(Arg.Any<int>());
        await repository.DidNotReceive().Update(Arg.Any<User>());
    }
}