// using Microsoft.Extensions.DependencyInjection;
// using FluentValidation;
// using FluentValidation.AspNetCore;
// using MediatR;
//
// namespace BookManager.Application
// {
//     public static class ApplicationModule
//     {
//         public static IServiceCollection AddApplication (this IServiceCollection services)
//         {
//             services
//                 .AddHandlers()
//                 .AddValidation();
//             return services;
//         }
//
//         private static IServiceCollection AddHandlers(this IServiceCollection services) 
//         {
//             services.AddMediatR(config =>
//             {
//                 config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>();
//             });
//
//             services
//                 .AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>,
//                     ValidateInsertCommandBehavior>();
//
//             return services;
//         }
//
//         private static IServiceCollection AddValidation(this IServiceCollection services)
//         {
//             services
//                 .AddFluentValidationAutoValidation()
//                 .AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
//             return services;
//         }
//     }
// }
