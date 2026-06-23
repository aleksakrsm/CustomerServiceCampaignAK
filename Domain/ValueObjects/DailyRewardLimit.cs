using Domain.Base;
using Domain.Guards;

namespace Domain.ValueObjects
{
    public class DailyRewardLimit : ValueObject
    {
        private DailyRewardLimit()
        {
            Value = 0;
        }

        private DailyRewardLimit(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public static DailyRewardLimit Create(int value)
        {
            Guard.Against.Negative(value, "Daily reward limit cannot be negative");

            return new DailyRewardLimit(value);
        }

        public static DailyRewardLimit? CreateOptional(int? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            return Create(value.Value);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();

        // TODO: Implicit conversion to string for convenience
        public static implicit operator string(DailyRewardLimit dailyRewardLimit) => dailyRewardLimit.Value.ToString();
    }
}
