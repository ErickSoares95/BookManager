using Bogus;
using BookManager.Application.Commands.BookCommands.DeleteBook;
using BookManager.Application.Commands.BookCommands.InsertBook;
using BookManager.Application.Commands.UserCommands.InsertUser;
using BookManager.Core.Entities;
using BookManager.Core.Enums;

namespace TestProject1BookManager.UnitTests.Fakes;

public class FakeDataHelper
{
    private static readonly Faker _faker = new Faker();

    private static Book _createFakeBook()
    {
        return new Book(
            _faker.Commerce.ProductName(),
            _faker.Name.FullName(),
            _faker.Random.Replace("###-#-##-######-#"),
            _faker.Date.Past(50).Year
        );
    }

    private static readonly Faker<Book> _bookFaker = new Faker<Book>()
        .CustomInstantiator(b => new Book(
            b.Commerce.ProductName(),
            b.Name.FullName(),
            b.Random.Replace("###-#-##-######-#"),
            b.Date.Past(50).Year
        ));

    private static readonly Faker<InsertBookCommand> _insertBookCommandFaker = new Faker<InsertBookCommand>()
            .RuleFor(b => b.Title, b => $"{b.Commerce.ProductAdjective()}  {b.Commerce.Product()}")
            .RuleFor(b => b.Author, f => f.Name.FullName())
            .RuleFor(b => b.ISBN, f => f.Random.Replace("###-#-##-######-#"))
            .RuleFor(b => b.PublicationYear, f => f.Date.Past(50).Year);

    private static readonly Faker<User> _userFaker = new Faker<User>()
        .CustomInstantiator(u => new User(
            u.Name.FullName(),
            u.Internet.Email(),
            u.Date.Past(30),
            u.Internet.Password(),
            u.Name.JobTitle()
        // u.PickRandom<Role>()
        ));

    private static readonly Faker<InsertUserCommand> _insertUserCommandFaker = new Faker<InsertUserCommand>()
        .RuleFor(u => u.FullName, f => f.Name.FullName())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.BirthDate, f => f.Date.Past(30));
    
    
    public static Book CreateBook() => _bookFaker.Generate();
    
    public static List<Book> CreateBooks() => _bookFaker.Generate(10);
    
    public static InsertBookCommand CreateFakeInsertBookCommand() => _insertBookCommandFaker.Generate();

    public static User CreateUser() => _userFaker.Generate();
    
    public static InsertUserCommand CreateFakeInsertUserCommand() => _insertUserCommandFaker.Generate();
}