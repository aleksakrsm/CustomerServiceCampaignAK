using FluentValidation;

namespace Application.Features.Campaigns.Commands.CreateReward
{
    public class CreateRewardCommandValidator : AbstractValidator<CreateRewardCommand>
    {
        public CreateRewardCommandValidator()
        {
            RuleFor(x => x.agentId).NotEmpty().WithMessage("AgentId is required.");
            RuleFor(x => x.customerId)
                .NotEmpty().WithMessage("CustomerId is required.")
                .GreaterThanOrEqualTo(1).WithMessage("CustomerId must be a positive integer.");
            RuleFor(x => x.description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
