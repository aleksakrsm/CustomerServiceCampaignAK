namespace Application.Features.Base
{
    public abstract record AddCommand : Command<Guid>
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
