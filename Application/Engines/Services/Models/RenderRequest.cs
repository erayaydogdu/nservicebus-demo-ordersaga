namespace OrderService.Application.Engines.Services.Models;

public class RenderRequest
{
    public string TemplateName { get; set; }
    public string Locale { get; set; } = "nl-NL";
    public object Data { get; set; }
}