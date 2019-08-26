using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPost.Modules.Parcels.Shared.Commands;
using NPost.Modules.Parcels.Shared.DTO;
using NPost.Modules.Parcels.Shared.Queries;
using NPost.Shared;

namespace NPost.Modules.Parcels.Controllers
{
    [ApiController]
    [Route("api/parcels")]
    public class ParcelsApiController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ParcelsApiController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        [HttpGet("_meta")]
        public ActionResult<string> Meta() => "Parcels module";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelDto>>> Get([FromQuery] GetParcels query)
        {
            var parcels = await _dispatcher.QueryAsync(query);

            return Ok(parcels);
        }
        
        [HttpGet("{parcelId}")]
        public async Task<ActionResult<IEnumerable<ParcelDto>>> Get([FromRoute] Guid parcelId)
        {
            var parcel = await _dispatcher.QueryAsync(new GetParcel(parcelId));
            if (parcel is null)
            {
                return NotFound();
            }

            return Ok(parcel);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AddParcel command)
        {
            await _dispatcher.SendAsync(command);
            Response.Headers.Add("Resource-ID", $"{command.ParcelId:N}");

            return CreatedAtAction(nameof(Get), new {parcelId = command.ParcelId}, null);
        }
        
        [HttpDelete("{parcelId}")]
        public async Task<ActionResult> Post([FromRoute] Guid parcelId)
        {
            await _dispatcher.SendAsync(new DeleteParcel(parcelId));

            return NoContent();
        }
    }
}