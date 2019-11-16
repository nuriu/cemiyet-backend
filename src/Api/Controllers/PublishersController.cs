using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Publishers.Commands.Add;
using Cemiyet.Application.Publishers.Commands.UpdatePartially;
using Cemiyet.Application.Publishers.Commands.Update;
using Cemiyet.Application.Publishers.Commands.DeleteOne;
using Cemiyet.Application.Publishers.Commands.DeleteMany;
using Cemiyet.Application.Publishers.Queries.List;
using Cemiyet.Application.Publishers.Queries.Details;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [PublishersExceptionFilter]
    public class PublishersController : CemiyetBaseController
    {
        public PublishersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data) => await Mediator.Send(data);

        [HttpGet]
        [ProducesResponseType(typeof(List<Publisher>), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<List<Publisher>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Publisher), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Publisher>> Details(Guid id) => await Mediator.Send(new DetailsQuery { Id = id });

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartially([FromRoute] Guid id,
                                                              [FromBody] UpdatePartiallyCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne(Guid id) => await Mediator.Send(new DeleteOneCommand {Id = id});

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data) => await Mediator.Send(data);
    }
}
