namespace Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(
            string toEmail,
            string otpCode,
            CancellationToken cancellationToken = default);
    }
}
