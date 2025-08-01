using BookManager.Application.Commands.LoanCommands.InsertLoan;
using BookManager.Core.Entities;
using BookManager.Core.Repository;
using NSubstitute;

namespace TestProject1BookManager.UnitTests.Application.LoanTests;

public class InsertLoanHandlerTests
{
    private const int ID = 1;
    
    [Fact]
    public async Task InputDataAreOk_Insert_Success_NSubistitute()
    {
        //Arrange
        var loanRepository = Substitute.For<ILoanRepository>();
        var bookRepository = Substitute.For<IBookRepository>();
        var userRepository = Substitute.For<IUserRepository>();
        
        loanRepository.Add(Arg.Any<Loan>()).Returns(Task.FromResult(ID));

        var command = new InsertLoanCommand();
        
        var handler = new InsertLoanCommandHandler(bookRepository, userRepository, loanRepository);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        
        await loanRepository.Received(ID).Add(Arg.Any<Loan>());
        await userRepository.Received(ID).Add(Arg.Any<User>());
        await bookRepository.Received(ID).Add(Arg.Any<Book>());
    }
}