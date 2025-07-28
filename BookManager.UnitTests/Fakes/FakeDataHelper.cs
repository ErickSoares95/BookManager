using Bogus;
using BookManager.Application.Commands.BookCommands.DeleteBook;
using BookManager.Application.Commands.BookCommands.InsertBook;
using BookManager.Core.Entities;

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
            .RuleFor(l => l.ISBN, f => f.Random.Replace("###-#-##-######-#"))
            .RuleFor(l => l.PublicationYear, f => f.Date.Past(50).Year);
    
    public static Book CreateBook() => _bookFaker.Generate();
    
    public static List<Book> CreateBooks() => _bookFaker.Generate(10);
    
    public static InsertBookCommand CreateFakeInsertBookCommand() => _insertBookCommandFaker.Generate();

}