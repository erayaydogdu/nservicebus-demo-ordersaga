using OrderService.Application.Managers.NotificationManager.Messages.Commands.Common;

namespace OrderService.Application.Managers.NotificationManager.Messages.Commands;

public class NotifyCustomer
{
    public string TemplateName { get; set; }
    public ContactDetails ContactDetails { get; set; }
}