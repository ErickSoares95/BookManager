using BookManager.Application.Commands.LoanCommands.UpdateLoan;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using NSubstitute;

namespace TestProject1BookManager.UnitTests.Application.LoanTests;

public class UpdateLoanHandlerTests
{
    private const int ID = 1;
    public const string LOAN_NOT_FOUND_MESSAGE = "Loan Not Found";

    [Fact]
    public async Task LoanExists_Update_Success_NSubstitute()
    {
        //Arrange
        var loan = new Loan(1, 2);
        
        var repository = Substitute.For<ILoanRepository>();
        repository.GetById(ID).Returns(Task.FromResult<Loan?>(loan));
        repository.Update(Arg.Any<Loan>()).Returns(Task.CompletedTask);
        
        var handler = new UpdateLoanCommandHandler(repository);

        var command = new UpdateLoanCommand();
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        await repository.Received(ID).GetById(ID);
        await repository.Received(ID).Update(Arg.Any<Loan>());
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task BookDoesNotExists_Update_Error_NSubstitute()
    {
        var repository = Substitute.For<ILoanRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Loan?)null));
        
        var handler = new UpdateLoanCommandHandler(repository);

        var command = new UpdateLoanCommand();
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(UpdateLoanCommandHandler.LOAN_NOT_FOUND_MESSAGE, result.Message);
        
        await repository.Received(ID).GetById(Arg.Any<int>());
        await repository.DidNotReceive().Update(Arg.Any<Loan>());
    }
}