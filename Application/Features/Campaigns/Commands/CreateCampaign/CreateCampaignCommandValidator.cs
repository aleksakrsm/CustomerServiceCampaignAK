using FluentValidation;

namespace Application.Features.Campaigns.Commands.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Campaign name is required.")
                .MaximumLength(100).WithMessage("Campaign name must not exceed 100 characters.");
            RuleFor(x => x.dailyLimit)
                .GreaterThanOrEqualTo(0).WithMessage("Daily limit must be a non-negative value.");
            RuleFor(x => x.endDate)
                .GreaterThanOrEqualTo(x => x.startDate).WithMessage("End date must be after the start date.");
        }
    }
}
