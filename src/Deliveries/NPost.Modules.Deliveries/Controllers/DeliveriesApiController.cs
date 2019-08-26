using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPost.Modules.Deliveries.Shared.Commands;
using NPost.Modules.Deliveries.Shared.DTO;
using NPost.Modules.Deliveries.Shared.Queries;
using NPost.Shared;

namespace NPost.Modules.Deliveries.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveriesApiController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public DeliveriesApiController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        [HttpGet("_meta")]
        public ActionResult<string> Meta() => "Deliveries module";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryDto>>> Get([FromQuery] GetDeliveries query)
        {
            var deliveries = await _dispatcher.QueryAsync(query);

            return Ok(deliveries);
        }

        [HttpGet("{deliveryId}")]
        public async Task<ActionResult<IEnumerable<DeliveryDto>>> Get([FromRoute] Guid deliveryId)
        {
            var delivery = await _dispatcher.QueryAsync(new GetDelivery(deliveryId));
            if (delivery is null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }

        [HttpPost]
        public async Task<ActionResult> Post(StartDelivery command)
        {
            await _dispatcher.SendAsync(command);
            Response.Headers.Add("Resource-ID", $"{command.DeliveryId:N}");
       
            return CreatedAtAction(nameof(Get), new {deliveryId = command.DeliveryId}, null);
        }     

        [HttpPost("{deliveryId}/complete")]
        public async Task<ActionResult> Post([FromRoute] Guid deliveryId)
        {
            await _dispatcher.SendAsync(new CompleteDelivery(deliveryId));

            return NoContent();
        }
        
        [HttpDelete("{deliveryId}")]
        public async Task<ActionResult> Delete([FromRoute] Guid deliveryId)
        {
            await _dispatcher.SendAsync(new CancelDelivery(deliveryId, "Delivery canceled."));

            return NoContent();
        }
    }
}