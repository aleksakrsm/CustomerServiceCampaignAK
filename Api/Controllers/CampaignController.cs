using Application.Features.Campaigns.Commands.CreateCampaign;
using Application.Features.Campaigns.Commands.CreateReward;
using Application.Features.Campaigns.Commands.DeleteReward;
using Application.Features.Campaigns.Queries.GetRewardById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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

        [HttpDelete("{id:guid}/rewards/{rewardId:guid}")]
        public async Task<ActionResult> DeleteReward(Guid id, Guid rewardId, CancellationToken cancellationToken = default)
        {
            var command = new DeleteRewardCommand(rewardId, id);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id:guid}/rewards/{rewardId:guid}")]
        public async Task<ActionResult> GetReward(Guid id, Guid rewardId, CancellationToken cancellationToken = default)
        {
            var command = new GetRewardByIdQuery(rewardId);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
