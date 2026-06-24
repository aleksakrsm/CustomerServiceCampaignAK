using Application.Abstractions.Persistence;
using Application.Features.Base;
using Application.Features.Campaigns.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Campaigns.Queries.GetRewardById
{
    public record GetRewardByIdQuery(Guid Id) : Query<RewardDto>
    {
    }

    public sealed class GetRewardByIdQueryHandler(IDbContext dbContext, IMapper mapper) : CachedQueryHandler<Reward, GetRewardByIdQuery, RewardDto>(dbContext, mapper)
    {
        protected override IQueryable<Reward> BuildQuery(GetRewardByIdQuery request)
        {
            return _dbContext.Set<Reward>()
                .Where(x => x.Id == request.Id);
        }
    }
}
