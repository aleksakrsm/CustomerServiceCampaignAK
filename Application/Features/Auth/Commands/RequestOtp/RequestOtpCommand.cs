using Application.Abstractions.Persistence;
using Application.Abstractions.Services;
using Application.Features.Auth.DTOs;
using Application.Features.Base;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.RequestOtp
{
    public record RequestOtpCommand : Command<RequestOtpDto>
    {
        public string Email { get; init; } = string.Empty;
    }

    public sealed class RequestOtpCommandHandler(
    IDbContext context,
    IEmailService emailService) : IRequestHandler<RequestOtpCommand, Result<RequestOtpDto>>
    {

        private static readonly RequestOtpDto _successResponse = new()
        {
            Message = "If this email is registered, an OTP has been sent",
        };

        public async Task<Result<RequestOtpDto>> Handle(
            RequestOtpCommand request,
            CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            var user = await context
                .Users.IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            // return same response for non existant users
            if (user == null)
            {
                return Result<RequestOtpDto>.Success(_successResponse);
            }

            var codes = await context
                .OtpCodes.IgnoreQueryFilters()
                .Where(o => o.Email == email && !o.IsUsed)
                .ToListAsync(cancellationToken);

            foreach (var code in codes)
            {
                code.MarkAsUsed();
            }


            var recentOtpCount = await context.OtpCodes.IgnoreQueryFilters()
                .CountAsync(o => o.Email == email  && o.CreatedAt > DateTime.UtcNow.AddMinutes(-15), cancellationToken);

            if (recentOtpCount >= 5)
            {
                return Result<RequestOtpDto>.Failure("Too many OTP requests. Please try again later.");
            }

            var otp = OtpCode.Create(email.Value);
            await context.OtpCodes.AddAsync(otp, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await emailService.SendOtpEmailAsync(email.Value, otp.Code, cancellationToken);

            return Result<RequestOtpDto>.Success(_successResponse);
        }
    }
}
