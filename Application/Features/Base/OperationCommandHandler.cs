using Application.Abstractions.Persistence;
using Domain.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Base
{
    public abstract class OperationCommandHandler<TEntity, TRequest>(IDbContext dbContext) : IRequestHandler<TRequest, Result>
    where TRequest : OperationCommand
    where TEntity : AggregateRoot
    {
        protected IDbContext DbContext => dbContext;
        public virtual async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entityName = typeof(TEntity).Name;

            var entity = await GetEntityByIdAsync(request, cancellationToken);
            if (entity == null)
            {
                return Result.Failure($"{entityName} with id {request.Id} was not found");
            }

            var validationResult = await ValidateBusinessRulesAsync(request, entity, cancellationToken);

            if (!validationResult.IsSuccess)
            {
                return Result.Failure(validationResult.Message!);
            }

            await Operation(entity, request, cancellationToken);
            //foreach (var e in DbContext.ChangeTracker.Entries())
            //{
            //    Console.WriteLine(
            //        $"{e.Entity.GetType().Name} {e.State}");
            //}
            await DbContext.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }

        protected virtual async Task<TEntity?> GetEntityByIdAsync(
            TRequest request,
            CancellationToken cancellationToken)
        {
            return await DbContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        }

        protected abstract Task Operation(
            TEntity entity,
            TRequest request,
            CancellationToken cancellationToken);

        protected virtual Task<Result> ValidateBusinessRulesAsync(
            TRequest request,
            TEntity entity,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success());
        }
    }
}
