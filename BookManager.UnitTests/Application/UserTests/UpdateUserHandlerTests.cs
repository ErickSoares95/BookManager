using BookManager.Application.Commands.UserCommands.UpdateUser;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using NSubstitute;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Application.UserTests;

public class UpdateUserHandlerTests
{
    private const int ID = 1;
    public const string USER_NOT_FOUND_MESSAGE = "User Not Found";

    [Fact]
    public async Task UserExists_Update_Success_NSubstitute()
    {
        //Arrange
        var user = FakeDataHelper.CreateUser();

        var repository = Substitute.For<IUserRepository>();
        repository.GetById(user.Id).Returns(Task.FromResult<User?>(user));
        repository.Update(Arg.Any<User>()).Returns(Task.CompletedTask);

        var handler = new UpdateUserCommandhandler(repository);

        var command = new UpdateUserCommand();
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        await repository.Received(ID).GetById(user.Id);
        await repository.Received(ID).Update(Arg.Any<User>());
        Assert.True(result.IsSuccess);
    }
}