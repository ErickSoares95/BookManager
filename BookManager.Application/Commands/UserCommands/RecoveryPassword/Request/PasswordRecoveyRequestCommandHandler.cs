using BookManager.Application.Models;
using BookManager.Core.Repository;
using BookManager.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BookManager.Application.Commands.UserCommands.RecoveryPassword.Request;

public class PasswordRecoveyRequestCommandHandler: IRequestHandler<PasswordRecoveyRequestCommand, ResultViewModel>
{
    public PasswordRecoveyRequestCommandHandler(IUserRepository userRepository, IMemoryCache memoryCache, IEmailService emailService)
    {
        _userRepository = userRepository;
        _memoryCache = memoryCache;
        _emailService = emailService;
    }

    private const string USER_NOT_FOUND_MESSAGE = "User not found";
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IEmailService _emailService;
    
    public async Task<ResultViewModel> Handle(PasswordRecoveyRequestCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByEmail(request.Email);
        
        if (user == null)
            return ResultViewModel.Error(USER_NOT_FOUND_MESSAGE);
        
        var code = new Random().Next(100000, 999999);

        var cacheKey = $"RecoveryCode:{request.Email}";
        
        _memoryCache.Set(cacheKey, code, TimeSpan.FromMinutes(10));
        
        await _emailService.SendAsync(request.Email, "Code from recovery", $"Your code from recovery is: {code}");
        
        return ResultViewModel.Success();
    }
}