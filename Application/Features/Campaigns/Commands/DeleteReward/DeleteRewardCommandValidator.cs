using FluentValidation;

namespace Application.Features.Campaigns.Commands.DeleteReward
{
    public class DeleteRewardCommandValidator : AbstractValidator<DeleteRewardCommand>
    {
        public DeleteRewardCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");
            RuleFor(x => x.rewardId)
                .NotEmpty().WithMessage("Reward Id is required.")
                .Must(id => id != Guid.Empty).WithMessage("Reward Id must be a valid GUID.");
        }
    }
}
