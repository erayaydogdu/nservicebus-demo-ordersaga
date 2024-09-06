using Fluid;
using OrderService.Application.Engines.Services.Models;

namespace OrderService.Application.Engines.Services;

public class LiquidTemplateEngine : ITemplateEngine
{
    public async Task<string> RenderAsync(RenderRequest request)
    {
        var parser = new FluidParser();

        //get template
        var path = $"Templates/{request.TemplateName}.liquid";
        var source = File.ReadAllText(path);
        var template = parser.Parse(source);

        //create context
        TemplateOptions options = new TemplateOptions();
        options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
        options.CultureInfo = new System.Globalization.CultureInfo(request.Locale);
        var context = new TemplateContext(request.Data, options, true);

        //render template
        return await template.RenderAsync(context);
    }
}