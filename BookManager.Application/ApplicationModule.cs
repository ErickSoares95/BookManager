using BookManager.Application.Commands.BookCommands.InsertBook;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            services
                .AddHandlers();
                // .AddValidation();
            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services) 
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<InsertBookCommand>();
            });

            return services;
        }
    }
}
