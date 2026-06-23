using Domain.Base;
using Domain.Guards;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Campaign : AggregateRoot
    {
        private Campaign()
        {
        }

        public string Name { get; private set; } = null!;
        public DailyRewardLimit DailyRewardLimit { get; private set; } = null!;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        private readonly List<Reward> _rewards = [];
        public IReadOnlyCollection<Reward> Rewards => _rewards.AsReadOnly();


        public static Campaign Create(
            string name,
            int dailyRewardLimit,
            DateTime startDate,
            DateTime endDate)
        {
            Guard.Against.NullOrWhiteSpace(name, "Campaign name is required");
            Guard.Against.Negative(dailyRewardLimit, "Daily reward limit cannot be negative");
            Guard.Against.InvalidCondition(startDate <= endDate, "Start date must be before end date");

            var campaign = new Campaign
            {
                Name = name,
                DailyRewardLimit = DailyRewardLimit.Create(dailyRewardLimit),
                StartDate = startDate,
                EndDate = endDate
            };

            return campaign;
        }

        public void CreateReward(Guid AgentId, long CustomerId, string Description)
        {
            var reward = Reward.Create(Id, AgentId, CustomerId, Description);
            _rewards.Add(reward);
        }

        public void DeleteReward(Guid rewardId)
        {
            _rewards.FirstOrDefault(x => x.Id == rewardId)?.Delete();
        }
    }
}
