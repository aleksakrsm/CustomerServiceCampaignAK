using Application.Mapping.AutoMapper;
using AutoMapper;
using Domain.Entities;
namespace Application.Features.Campaigns.DTOs
{
    public record RewardDto : IMap
    {
        public Guid Id { get; init; }
        public Guid CampaignId { get; init; }
        public Guid AgentId { get; init; }
        public long CustomerId { get; init; }
        public string Description { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public bool IsDeleted { get; init; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reward, RewardDto>();
        }
    }
}
