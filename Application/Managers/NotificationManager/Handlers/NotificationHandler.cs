using OrderService.Application.Engines.Services;
using OrderService.Application.Engines.Services.Models;
using OrderService.Application.Managers.NotificationManager.Messages.Commands;
using OrderService.Application.Managers.NotificationManager.Messages.Events;
using OrderService.Application.Utilities.EmailUtility.Messages.Commands;

namespace OrderService.Application.Managers.NotificationManager.Handlers;

public class NotificationHandler : IHandleMessages<NotifyCustomer>
{
    private readonly ILogger<NotificationHandler> _logger;
    private readonly ITemplateEngine _engine;

    public NotificationHandler(ILogger<NotificationHandler> logger, ITemplateEngine engine)
    {
        _logger = logger;
        _engine = engine;
    }

    public async Task Handle(NotifyCustomer message, IMessageHandlerContext context)
    {
        _logger.LogInformation("Going to send a {TemplateName} Email.", message.TemplateName);

        // step 1: render notification
        var body = await _engine.RenderAsync(new RenderRequest
        {
            TemplateName = message.TemplateName,
            Data = new
            {
                FirstName = message.ContactDetails.FirstName,
                LastName = message.ContactDetails.LastName
            }
        });

        // step 2: send email
        await context.SendLocal<SendEmail>(msg =>
        {
            msg.To = message.ContactDetails.Email;
            msg.From = "no-reply@company.com";
            msg.Subject = "Order confirmation";
            msg.Body = body;
        });

        // step 3. Email has been sent
        await context.Reply<IEmailSent>(x => x.TemplateName = message.TemplateName);
    }
}