using Application.Abstractions.Persistence;
using Application.Abstractions.Services;
using Application.Features.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Campaigns.Commands.CreateReward
{
    public record CreateRewardCommand(Guid agentId, long customerId, string description) : OperationCommand
    {
    }

    public sealed class CreateRewardCommandHandler(IDbContext context, ICustomerService customerService) : OperationCommandHandler<Campaign, CreateRewardCommand>(context)
    {
        protected override async Task Operation(Campaign entity, CreateRewardCommand request, CancellationToken cancellationToken)
        {
            entity.CreateReward(request.agentId, request.customerId, request.description);
            await Task.CompletedTask;
        }

        protected override async Task<Campaign?> GetEntityByIdAsync(
            CreateRewardCommand request,
            CancellationToken cancellationToken)
        {
            return await DbContext.Campaigns.Include(x=>x.Rewards).FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        }

        protected override async Task<Result> ValidateBusinessRulesAsync(
        CreateRewardCommand request,
        Campaign entity,
        CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerByIdAsync(request.customerId, cancellationToken);
            if (customer is null)
            {
                return Result.Failure($"Customer with id '{request.customerId}' does not exist.");
            }

            var customerAwarded = await DbContext.Rewards.AnyAsync(r => r.CustomerId == request.customerId && r.CampaignId == entity.Id, cancellationToken);
            if (customerAwarded)
            {
                return Result.Failure($"Customer with id '{request.customerId}' has already been awarded a reward in this campaign.");
            }

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var agentDailyRewardsCount = await DbContext.Rewards
                .CountAsync(r => r.AgentId == request.agentId && r.CampaignId == entity.Id
                              && r.CreatedAt >= today
                              && r.CreatedAt < tomorrow,
                            cancellationToken);
            if (agentDailyRewardsCount >= entity.DailyRewardLimit.Value)
            {
                return Result.Failure($"Agent with id '{request.agentId}' has reached the daily reward limit.");
            }

            return Result.Success();
        }
    }
}
