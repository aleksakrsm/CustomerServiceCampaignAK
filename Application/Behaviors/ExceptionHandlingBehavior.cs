using Application.Exceptions;
using Application.Features.Base;
using Domain.Exceptions;
using MediatR;

namespace Application.Behaviors
{
    public class ExceptionHandlingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (AlreadyExistsException ex)
            {
                return CreateFailureResultOrThrow(ex.Message);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return CreateFailureResultOrThrow(ex.Message);
            }
            catch (BusinessRuleValidationException ex)
            {
                return CreateFailureResultOrThrow(ex.Message);
            }
        }

        private static TResponse CreateFailureResultOrThrow(string errorMessage)
        {
            var result = CreateFailureResult(errorMessage);
            if (result is not null)
            {
                return result;
            }

            throw new InvalidOperationException(
                $"Cannot create failure result for response type {typeof(TResponse).Name}");
        }

        private static TResponse? CreateFailureResult(string errorMessage)
        {
            var responseType = typeof(TResponse);

            // Handle non-generic Result
            if (responseType == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(errorMessage);
            }

            // Handle Result<T>
            if (
                responseType.IsGenericType
                && responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var innerType = responseType.GetGenericArguments()[0];
                var failureMethod = typeof(Result<>)
                    .MakeGenericType(innerType)
                    .GetMethod(nameof(Result<object>.Failure), [typeof(string)]);

                if (failureMethod is not null)
                {
                    return (TResponse?)failureMethod.Invoke(null, [errorMessage]);
                }

                // Fallback to string-based failure method
                failureMethod = typeof(Result<>)
                    .MakeGenericType(innerType)
                    .GetMethod(nameof(Result<object>.Failure), [typeof(string)]);

                if (failureMethod is not null)
                {
                    return (TResponse?)failureMethod.Invoke(null, [errorMessage]);
                }
            }

            // For non-Result response types, return null to indicate we can't handle it
            return default;
        }
    }
}
