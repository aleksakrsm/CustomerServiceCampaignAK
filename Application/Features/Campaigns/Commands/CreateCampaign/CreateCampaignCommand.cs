using Application.Abstractions.Persistence;
using Application.Features.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Campaigns.Commands.CreateCampaign
{
    public record CreateCampaignCommand(string name, int dailyLimit, DateTime startDate, DateTime endDate) : AddCommand
    {
    }
    public sealed class CreateCampaignCommandHandler(IDbContext dbContext) : AddCommandHandler<Campaign, CreateCampaignCommand>(dbContext)
    {
        protected override Campaign Create(CreateCampaignCommand request)
        {
            return Campaign.Create(request.name, request.dailyLimit, request.startDate, request.endDate);
        }

        protected override async Task<Result> ValidateBusinessRulesAsync(
            CreateCampaignCommand request,
            CancellationToken cancellationToken)
        {
            //check if campaign with same name already exists
            var campaignExists = await DbContext.Campaigns.AnyAsync(c => c.Name == request.name, cancellationToken);
            if (campaignExists)
            {
                return Result.Failure($"Campaign with name '{request.name}' already exists.");
            }

            return Result.Success();
        }
    }
}
