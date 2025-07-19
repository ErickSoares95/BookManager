using System.Text.Json.Serialization;
using BookManager.API.ExceptionHandler;
using BookManager.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
    //Caso um dia precise de uma consulta ciclica
    // .AddJsonOptions(options =>
    // {
    //     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    //     // Opcional: para formatar o JSON com identação para facilitar a leitura
    //     // options.JsonSerializerOptions.WriteIndented = true;
    // });

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// builder.Services.AddExceptionHandler<ApiExceptionHandler>();
// builder.Services.AddProblemDetails();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
