using FluentValidation;

namespace Application.Features.Campaigns.Queries.GetRewardById
{
    public class GetRewardByIdQueryValidator : AbstractValidator<GetRewardByIdQuery>
    {
        public GetRewardByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Reward Id is required.")
                .Must(id => id != Guid.Empty).WithMessage("Reward Id cannot be an empty GUID.");
        }
    }
}
