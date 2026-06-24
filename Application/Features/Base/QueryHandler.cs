using Application.Abstractions.Persistence;
using AutoMapper;
using Domain.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Base
{
    public abstract class CachedQueryHandler<TEntity, TRequest, TResponse>
    : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : Query<TResponse>
    where TEntity : Entity
    where TResponse : class
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected virtual string NotFoundMessage => $"{typeof(TEntity).Name} not found";

        protected CachedQueryHandler(
            IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<Result<TResponse>> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            var query = BuildQuery(request).AsNoTracking();
            var entity = await query.FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                return Result<TResponse>.Failure(NotFoundMessage);
            }

            var dto = _mapper.Map<TResponse>(entity);

            return Result<TResponse>.Success(dto);
        }

        protected abstract IQueryable<TEntity> BuildQuery(TRequest request);
    }
}
