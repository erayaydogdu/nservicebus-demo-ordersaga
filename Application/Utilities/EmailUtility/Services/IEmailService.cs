using OrderService.Application.Utilities.EmailUtility.Models;

namespace OrderService.Application.Utilities.EmailUtility.Services;

public interface IEmailService
{
    Task SendAsync(MailMessage message);
}
