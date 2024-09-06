using OrderService.Application.Managers.NotificationManager.Messages.Commands;
using OrderService.Application.Managers.NotificationManager.Messages.Commands.Common;
using OrderService.Application.Managers.NotificationManager.Messages.Events;
using OrderService.Application.Managers.OrderManager.Messages.Commands;

namespace OrderService.Application.Managers.OrderManager.Sagas;

public class OrderSaga : Saga<OrderSagaData>,
     IAmStartedByMessages<PlaceOrder>,
     IHandleMessages<IEmailSent>
{
    private readonly ILogger<OrderSaga> _logger;

    public OrderSaga(ILogger<OrderSaga> logger)
    {
        _logger = logger;
    }
    
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
    {
        mapper.ConfigureMapping<PlaceOrder>(message => message.OrderId).ToSaga(saga => saga.OrderId);
    }

    public Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
        _logger.LogInformation("Order placed");

        //send order confirmation
        return context.SendLocal<NotifyCustomer>(x =>
        {
            x.TemplateName = "OrderConfirmation";
            x.ContactDetails = new ContactDetails
            {
                FirstName = "John",
                LastName = "Travolta",
                Email = "john@travolta.com",
                Phone = "0663322111"
            };
        });
    }

    public Task Handle(IEmailSent message, IMessageHandlerContext context)
    {
        _logger.LogInformation("Order confirmation has been sent. Mark Saga Complete!");
        MarkAsComplete();
        return Task.CompletedTask;
    }
}