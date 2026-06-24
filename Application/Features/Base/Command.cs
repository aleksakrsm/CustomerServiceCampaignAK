using MediatR;

namespace Application.Features.Base
{
    public abstract record Command<TResponse> : IRequest<Result<TResponse>>
    {
    }
    public abstract record Command : IRequest<Result>
    {
    }
}
