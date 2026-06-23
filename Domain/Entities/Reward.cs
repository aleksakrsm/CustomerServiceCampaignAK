using Domain.Base;
using Domain.Guards;

namespace Domain.Entities
{
    public class Reward : Entity
    {
        private Reward()
        {
        }

        public Guid CampaignId { get; private set; }
        public Guid AgentId { get; private set; }
        public long CustomerId { get; private set; }
        public string Description { get; private set; } = null!;

        public virtual Campaign Campaign { get; private set; } = null!;
        internal static Reward Create(Guid campaignId, Guid agentId, long customerId, string description)
        {
            Guard.Against.NullOrWhiteSpace(description, "Description is required");
            Guard.Against.Empty(campaignId, "Campaign ID is required");
            Guard.Against.Empty(agentId, "Agent ID is required");
            Guard.Against.StringTooLong(description, 200, "Description is too long");

            return new Reward
            {
                CampaignId = campaignId,
                AgentId = agentId,
                CustomerId = customerId,
                Description = description.Trim(),
            };
        }
    }
}
