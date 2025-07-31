using FluentAssertions;
using TestProject1BookManager.UnitTests.Fakes;

namespace TestProject1BookManager.UnitTests.Core;

public class UserTests
{
    [Fact]
    public void BookIsCreated_Update_Success()
    {
        //Arrange
        var user = FakeDataHelper.CreateUser();
        var previousName = user.FullName;
        var previousEmail = user.Email;
        var previousBirthDate = user.BirthDate;
        
        const string newName = "erick Soares";
        const string newEmail = "ericklira99@gmail.com";
        var newBirthDate = new DateTime(1995, 5, 17);
        
        //Act
        user.Update(newName,newEmail,newBirthDate);
        
        //assert
        user.FullName.Should().NotBe(previousName);
        user.Email.Should().NotBe(previousEmail);
        user.BirthDate.Should().NotBe(previousBirthDate);
    }
}