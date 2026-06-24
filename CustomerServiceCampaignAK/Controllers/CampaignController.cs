using Application.Features.Campaigns.Commands.CreateCampaign;
using Application.Features.Campaigns.Commands.CreateReward;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceCampaignAK.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CampaignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCampaign([FromBody] CreateCampaignCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("{id:guid}/rewards")]
        public async Task<ActionResult> CreateReward(Guid id, [FromBody] CreateRewardCommand command, CancellationToken cancellationToken = default)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId, cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Message);
        }
    }
}
