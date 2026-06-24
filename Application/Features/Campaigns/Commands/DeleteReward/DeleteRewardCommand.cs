using Application.Abstractions.Persistence;
using Application.Features.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Campaigns.Commands.DeleteReward
{
    public record DeleteRewardCommand : OperationCommand
    {
        public Guid rewardId { get; set; }
        public DeleteRewardCommand(Guid rewardId, Guid campaignId)
        {
            this.Id = campaignId;
            this.rewardId = rewardId;
        }
    }

    public sealed class DeleteRewardCommandHandler(IDbContext context) : OperationCommandHandler<Campaign, DeleteRewardCommand>(context)
    {
        protected override async Task Operation(Campaign entity, DeleteRewardCommand request, CancellationToken cancellationToken)
        {
            entity.DeleteReward(request.rewardId);
            await Task.CompletedTask;
        }

        protected override async Task<Campaign?> GetEntityByIdAsync(
            DeleteRewardCommand request,
            CancellationToken cancellationToken)
        {
            return await DbContext.Campaigns.Include(x => x.Rewards).FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        }

        protected override async Task<Result> ValidateBusinessRulesAsync(
        DeleteRewardCommand request,
        Campaign entity,
        CancellationToken cancellationToken)
        {
            var rewardExists = await DbContext.Rewards.AnyAsync(r => r.Id == request.rewardId && r.CampaignId == entity.Id, cancellationToken);
            if (!rewardExists)
            {
                return Result.Failure($"Reward with id '{request.rewardId}' not found.");
            }

            return Result.Success();
        }
    }
}
