using BookManager.Application.Models;
using BookManager.Core.Repository;
using BookManager.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BookManager.Application.Commands.UserCommands.RecoveryPassword.Validate;

public class PasswordRecoveyValidateCommandHandler : IRequestHandler<PasswordRecoveyValidateCommand, ResultViewModel>
{
    public PasswordRecoveyValidateCommandHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    private readonly IMemoryCache _memoryCache;
        
    public async Task<ResultViewModel> Handle(PasswordRecoveyValidateCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"RecoveryCode:{request.Email}";
        if (!_memoryCache.TryGetValue(cacheKey, out string? code) || code != request.Code)
        {
            ResultViewModel.Error("Invalid code");
        }

        return ResultViewModel.Success();
    }
}