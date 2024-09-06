using System.Text;
using OrderService.Application.Utilities.EmailUtility.Models;

namespace OrderService.Application.Utilities.EmailUtility.Services;

public class EmailService : IEmailService
{
    public Task SendAsync(MailMessage message)
    {
        File.WriteAllText($"Emails/{Guid.NewGuid()}.txt", CreateMailContent(message));
        return Task.CompletedTask;
    }

    private string CreateMailContent(MailMessage message)
    {
        var sb = new StringBuilder();
        sb.AppendLine("To:");
        sb.AppendLine(message.To);
        sb.AppendLine();
        sb.AppendLine("From:");
        sb.AppendLine(message.From);
        sb.AppendLine();
        sb.AppendLine("Subject:");
        sb.AppendLine(message.Subject);
        sb.AppendLine();
        sb.AppendLine("Body:");
        sb.AppendLine(message.Body);
        return sb.ToString();
    }
}
