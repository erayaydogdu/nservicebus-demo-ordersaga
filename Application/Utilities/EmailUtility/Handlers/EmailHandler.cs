using OrderService.Application.Utilities.EmailUtility.Messages.Commands;
using OrderService.Application.Utilities.EmailUtility.Models;
using OrderService.Application.Utilities.EmailUtility.Services;

namespace OrderService.Application.Utilities.EmailUtility.Handlers;

public class EmailHandler : IHandleMessages<SendEmail>
{
    private readonly IEmailService _sender;

    public EmailHandler(IEmailService sender)
    {
        _sender = sender;
    }

    public Task Handle(SendEmail message, IMessageHandlerContext context)
    {
        var msg = new MailMessage
        {
            To = message.To,
            From = message.From,
            Subject = message.Subject,
            Body = message.Body
        };

        return _sender.SendAsync(msg);
    }
}