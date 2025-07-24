using BookManager.Core.Entities;
using BookManager.Core.Enums;

namespace TestProject1BookManager.UnitTests.Core;


public class BookTests
{   
    [Fact]
    public void BookIsCreated_StartLoan_Success()
    {
        //Arrange
         var book = new Book("Harry Potter", "J.K Rowling", "9788869183157", 1997);
        
        //Act
        book.StartLoan();
        
        //Assert
        Assert.Equal(BookStateEnum.Reserved, book.Status);
        Assert.True(book.Status == BookStateEnum.Reserved);
    }
    
    [Fact]
    public void BookIsInInvalidState_Start_ThrowsException()
    {
        //arrange
         var book = new Book("Harry Potter", "J.K Rowling", "9788869183157", 1997);
         book.StartLoan();
         
         //Act + Assert
         Action? start = book.StartLoan;
         
         var exception = Assert.Throws<InvalidOperationException>(start);
         Assert.Equal(Book.INVALID_STATE_MESSAGE, exception.Message);
         
    }
}