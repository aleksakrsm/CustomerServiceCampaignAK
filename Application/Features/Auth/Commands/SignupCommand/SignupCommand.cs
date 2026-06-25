using Application.Abstractions.Persistence;
using Application.Features.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.SignupCommand
{
    public record SignupCommand(string Email) : AddCommand
    {
    }
    public sealed class SignupCommandHandler(IDbContext dbContext) : AddCommandHandler<User, SignupCommand>(dbContext)
    {
        protected override User Create(SignupCommand request)
        {
            return User.Create(request.Email, UserRole.Regular);
        }

        protected override async Task<Result> ValidateBusinessRulesAsync(
            SignupCommand request,
            CancellationToken cancellationToken)
        {
            //check if user with same email already exists
            var userExists = await DbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);
            if (userExists)
            {
                return Result.Failure($"User with email '{request.Email}' already exists.");
            }

            return Result.Success();
        }
    }
}
