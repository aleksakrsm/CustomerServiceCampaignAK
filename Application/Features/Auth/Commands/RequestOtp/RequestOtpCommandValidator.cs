using FluentValidation;

namespace Application.Features.Auth.Commands.RequestOtp
{
    public class RequestOtpCommandValidator : AbstractValidator<RequestOtpCommand>
    {
        public RequestOtpCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
