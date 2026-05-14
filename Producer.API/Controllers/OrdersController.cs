using Infrastructure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;

namespace OrderProducer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IProducer _producer;
        public OrdersController(IProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreatedEvent order)
        {  
            _producer.PublishAsync(order);

            return Ok("Order Event Published");
        }
    }
}
