using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookManager.Infrastructure.Notifications;

public interface IEmailService
{
    Task SendAsync(string email, string subject, string message);
}