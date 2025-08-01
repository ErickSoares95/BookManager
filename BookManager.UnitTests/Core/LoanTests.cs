using BookManager.Core.Entities;
using BookManager.Core.Enums;
using FluentAssertions;

namespace TestProject1BookManager.UnitTests.Core;

public class LoanTests
{
    [Fact]
    public void LoanIsCreated_CompleteLoan_Success()
    {
        //Arrange
        var loan = new Loan(1, 2);
        
        //Act
        loan.CompleteLoan();
        
        //Assert
        Assert.Equal(LoanStateEnum.Complete, loan.Status);
        Assert.False(loan.Status != LoanStateEnum.Complete);
    }
    
    [Fact]
    public void LoanIsInInvalidState_Start_ThrowsException()
    {
        //Arrange
        var loan = new Loan(1, 2);
        loan.CompleteLoan();
        
        //Act + Assert
        Action? complete = loan.CompleteLoan;
        
        var exception = Assert.Throws<InvalidOperationException>(complete);
        Assert.Equal(Loan.INVALID_STATE_MESSAGE, exception.Message);
        
        //Assert with FluentAssertions
        complete.Should()
            .Throw<InvalidOperationException>()
            .WithMessage(Loan.INVALID_STATE_MESSAGE);
    }
}