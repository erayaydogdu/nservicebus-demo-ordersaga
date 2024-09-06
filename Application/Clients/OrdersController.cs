using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Clients.Models;
using OrderService.Application.Managers.OrderManager.Messages.Commands;

namespace OrderService.Application.Clients;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IMessageSession _messageSession;

    public OrdersController(ILogger<OrdersController> logger, IMessageSession messageSession)
    {
        _logger = logger;
        _messageSession = messageSession;
    }

    [HttpPost]
    public Task PlaceOrder(OrderRequest request)
    {
        //process order
        return _messageSession.SendLocal<PlaceOrder>(x => { x.OrderId = request.OrderId; });
    }
}