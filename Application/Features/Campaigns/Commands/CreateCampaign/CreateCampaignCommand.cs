using Application.Abstractions.Persistence;
using Application.Features.Base;
using Domain.Entities;

namespace Application.Features.Campaigns.Commands.CreateCampaign
{
    public record CreateCampaignCommand(string name, int dailyLimit, DateTime startDate, DateTime endDate) : AddCommand
    {
    }
    public class CreateCampaignCommandHandler(IDbContext dbContext) : AddCommandHandler<Campaign, CreateCampaignCommand>(dbContext)
    {
        protected override Campaign Create(CreateCampaignCommand request)
        {
            return Campaign.Create(request.name, request.dailyLimit, request.startDate, request.endDate);
        }
    }
}
