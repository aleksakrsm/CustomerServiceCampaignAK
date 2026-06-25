using Application.Abstractions.Persistence;
using Application.Abstractions.Services;
using Application.Features.Auth.DTOs;
using Application.Features.Base;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.VerifyOtp
{
    public record VerifyOtpCommand(string Email, string OtpCode) : Command<VerifyOtpDto>
    {
    }

    public sealed class VerifyOtpCommandHandler(IDbContext context, IJwtService jwtService) : IRequestHandler<VerifyOtpCommand, Result<VerifyOtpDto>>
    {
        public async Task<Result<VerifyOtpDto>> Handle(
            VerifyOtpCommand request,
            CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            var user = await context
                .Users.IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (user == null)
            {
                return Result<VerifyOtpDto>.Failure("Invalid credentials");
            }

            // Find valid OTP
            var otp = await context
                .OtpCodes.IgnoreQueryFilters()
                .Where(o => o.Email == email && !o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (otp == null)
            {
                return Result<VerifyOtpDto>.Failure("Invalid or expired OTP");
            }

            var verificationResult = otp.VerifyAndUse(request.OtpCode);

            if (verificationResult != OtpVerificationResult.Success)
            {
                var errorMessage = verificationResult switch
                {
                    OtpVerificationResult.InvalidCode => "Invalid or expired OTP",
                    OtpVerificationResult.Expired => "OTP has expired. Please request a new one.",
                    OtpVerificationResult.AlreadyUsed =>
                        "OTP has already been used. Please request a new one.",
                    _ => "Invalid or expired OTP",
                };

                await context.SaveChangesAsync(cancellationToken);
                return Result<VerifyOtpDto>.Failure(errorMessage);
            }

            var accessTokenResult = jwtService.GenerateAccessToken(user);


            await context.SaveChangesAsync(cancellationToken);

            return Result<VerifyOtpDto>.Success(
                new VerifyOtpDto
                {
                    AccessToken = accessTokenResult.Token,
                });
        }
    }
}
