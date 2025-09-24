# WEB API - ASP .NET Core 8.0.13

The goal of this project is to become more familiar with ASP.NET Core OpenApi 8.0.13 technology. The code-first approach was used to manage the SQL Server database and clean architecture was used.

### Resources

- ASP .NET Core OpenApi 8.0.13
- EF Core Design 8.0.13
- MediatR 12.4.1
- SQL Server
- Swashbuckle 8.1.4
- FluentValidation 11.3.0
- Authentication.JwtBearer 8.0.19
- SqlServer 8.0.13
- SendGrid 1.0.1
- FluentAssertions 8.5.0
- XUnit 2.5.3
- Bogus 35.6.3
- Moq 4.20.72
- NSubstitute 5.3.0



### First Steps

#### How to Run the Project

To get the API running locally, follow these steps:

1. **Clone the Repository**:

   git clone [YOUR_REPOSITORY_URL]
   cd [YOUR_FOLDER_NAME]

2. **Restore Dependencies**:

   Run the command below to install all the necessary NuGet packages.
   dotnet restore

3. **Configure the Database (optional)**:

   If the API uses Entity Framework Core, apply the migrations to create the database.
   dotnet ef database update

4. **Run the API**:
   Start the application. The API will be available at `https://localhost:<port>`.
   dotnet run
