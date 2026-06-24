using MediatR;

namespace Application.Features.Base
{
    public abstract record Query<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
