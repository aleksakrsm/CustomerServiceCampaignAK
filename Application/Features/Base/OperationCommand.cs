namespace Application.Features.Base
{
    public abstract record OperationCommand : Command
    {
        public Guid Id { get; set; }
    }
}
