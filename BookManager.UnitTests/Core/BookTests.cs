using BookManager.Core.Entities;
using BookManager.Core.Enums;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Core;


public class BookTests
{   
    [Fact]
    public void BookIsCreated_StartLoan_Success()
    {
        //Arrange
        var book = FakeDataHelper.CreateBook();
        
        //Act
        book.StartLoan();
        
        //Assert
        Assert.Equal(BookStateEnum.Reserved, book.Status);
        Assert.True(book.Status == BookStateEnum.Reserved);
        
        //Assert with FluentAssertions
        book.Status.Should().Be(BookStateEnum.Reserved);
        book.Status.Should().NotBe(BookStateEnum.Available);
    }
    
    [Fact]
    public void BookIsInInvalidState_Start_ThrowsException()
    {
        //arrange
        var book = FakeDataHelper.CreateBook();
         book.StartLoan();
         
         //Act + Assert
         Action? start = book.StartLoan;
         
         var exception = Assert.Throws<InvalidOperationException>(start);
         Assert.Equal(Book.INVALID_STATE_MESSAGE, exception.Message);
         
        //Assert with FluentAssertions
        start.Should()
            .Throw<InvalidOperationException>()
            .WithMessage(Book.INVALID_STATE_MESSAGE);
         
    }
}