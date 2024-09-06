namespace OrderService.Application.Utilities.EmailUtility.Messages.Commands;

public class SendEmail
{
    public string To { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}