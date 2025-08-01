using BookManager.Application.Commands.LoanCommands.CompleteLoan;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using FluentAssertions;
using NSubstitute;

namespace TestProject1BookManager.UnitTests.Application.LoanTests;

public class CompleteLoanHandlerTests
{
    private const int ID = 1;
    public const string LOAN_NOT_FOUND_MESSAGE = "Loan Not Found";
    [Fact]
    public async Task LoanExists_CompleteLoanHandler_ShouldReturnTrue()
    {
        //Arrange
        var loan = new Loan(1, 2);
        
        var repository = Substitute.For<ILoanRepository>();
        
        repository.GetById(ID).Returns(Task.FromResult<Loan?>(loan));
        repository.Update(Arg.Any<Loan>()).Returns(Task.CompletedTask);
        
        var handler = new CompleteLoanCommandHandler(repository);
        
        var command = new CompleteLoanCommand(ID);
        
        //Act
        var result = await handler.Handle(command, new  CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        await repository.Received(ID).GetById(ID);
        await repository.Received(ID).Update(Arg.Any<Loan>());
        
        //Assert with FluentAssertions
        result.IsSuccess.Should().BeTrue();
    }
}