namespace OrderService.Application.Managers.NotificationManager.Messages.Events;

public interface IEmailSent
{
    public string TemplateName { get; set; }
}