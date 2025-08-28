using BookManager.Application.Models;
using BookManager.Core.Repository;
using BookManager.Infrastructure.Auth;
using BookManager.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BookManager.Application.Commands.UserCommands.RecoveryPassword.ChangePassword;

public class PasswordRecoveyChangeCommandHandler : IRequestHandler<PasswordRecoveyChangeCommand, ResultViewModel>
{
    public PasswordRecoveyChangeCommandHandler(IMemoryCache memoryCache, IUserRepository userRepository,
        IEmailService emailService, IAuthService authService)
    {
        _memoryCache = memoryCache;
        _userRepository = userRepository;
        _emailService = emailService;
        _authService = authService;
    }
    private const string USER_NOT_FOUND_MESSAGE = "User not found";
    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IAuthService _authService;
    
    public async Task<ResultViewModel> Handle(PasswordRecoveyChangeCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"RecoveryCode:{request.Email}";
        
        if (!_memoryCache.TryGetValue(cacheKey, out string? code) || code != request.Code)
            ResultViewModel.Error("Invalid code");
        
        _memoryCache.Remove(cacheKey);
        
        var user = await _userRepository.GetByEmail(request.Email);
        if (user == null)
            return ResultViewModel.Error(USER_NOT_FOUND_MESSAGE);
        
        var hash = _authService.ComputeHash(request.NewPassword);

        user.UpdatePassword(hash);
        
        await _userRepository.Update(user);
        
        return ResultViewModel.Success();
    }
}