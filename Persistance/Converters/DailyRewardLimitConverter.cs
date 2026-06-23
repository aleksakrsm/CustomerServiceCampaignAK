using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Converters
{
    public sealed class DailyRewardLimitConverter : ValueConverter<DailyRewardLimit, int>
    {
        public DailyRewardLimitConverter()
            : base(
                dailyRewardLimit => dailyRewardLimit.Value, // to provider (database)
                value => DailyRewardLimit.Create(value)) // from provider
        {
        }
    }
}
