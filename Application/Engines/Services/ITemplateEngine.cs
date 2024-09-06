using OrderService.Application.Engines.Services.Models;

namespace OrderService.Application.Engines.Services;

public interface ITemplateEngine
{
    public Task<string> RenderAsync(RenderRequest context);
}