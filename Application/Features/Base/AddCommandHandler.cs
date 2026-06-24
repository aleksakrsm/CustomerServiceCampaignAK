using Application.Abstractions.Persistence;
using Application.Exceptions;
using Domain.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Base
{
    public abstract class AddCommandHandler<TEntity, TRequest>(IDbContext dbContext) : IRequestHandler<TRequest, Result<Guid>>
    where TRequest : AddCommand
    where TEntity : AggregateRoot
    {
        protected IDbContext DbContext => dbContext;

        public virtual async Task<Result<Guid>> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            await CheckEntityExistsAsync(request, request.Id, cancellationToken);

            var validationResult = await ValidateBusinessRulesAsync(request, cancellationToken);
            if (!validationResult.IsSuccess)
            {
                return Result<Guid>.Failure(validationResult.Message);
            }

            var entity = Create(request);

            await SaveChangesAsync(entity, request, cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }

        protected virtual async Task CheckEntityExistsAsync(
            TRequest request,
            Guid entityId,
            CancellationToken cancellationToken)
        {
            var found = await DbContext
                .Set<TEntity>()
                .AnyAsync(x => x.Id == request.Id, cancellationToken);
            if (found)
            {
                throw new AlreadyExistsException(typeof(TEntity).Name, entityId);
            }
        }

        protected virtual async Task SaveChangesAsync(
            TEntity entity,
            TRequest request,
            CancellationToken cancellationToken)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        protected virtual Task<Result> ValidateBusinessRulesAsync(
            TRequest request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success());
        }

        protected abstract TEntity Create(TRequest request);
    }
}
