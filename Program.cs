using OrderService.Application.Engines;
using OrderService.Application.Engines.Services;
using OrderService.Application.Utilities;
using OrderService.Application.Utilities.EmailUtility.Services;

namespace OrderService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseNServiceBus(context =>
        {
            var endpointConfiguration = new EndpointConfiguration("Samples.ASPNETCore.Sender");
            var transport = endpointConfiguration.UseTransport(new LearningTransport());
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();
            // transport.RouteToEndpoint(
            //     assembly: typeof(PlaceOrder).Assembly,
            //     destination: "Samples.ASPNETCore.Endpoint");

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(property => property.Name.EndsWith("Commands"));
            conventions.DefiningEventsAs(property => property.Name.EndsWith("Events"));
            // endpointConfiguration.SendOnly();

            endpointConfiguration.UsePersistence<LearningPersistence>();

            return endpointConfiguration;
        });
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //register services
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<ITemplateEngine, LiquidTemplateEngine>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
