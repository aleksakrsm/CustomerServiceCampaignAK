using Application.Abstractions.Services;
namespace Infrastructure.Services
{
    public class EmailServiceSimulation : IEmailService
    {
        public Task SendOtpEmailAsync(string toEmail, string otpCode, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[SIMULATION] OTP for {toEmail} is: {otpCode}");
            return Task.CompletedTask;
        }
    }
}
