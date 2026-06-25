using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.DTOs
{
    public record VerifyOtpDto
    {
        [Required]
        public string AccessToken { get; init; } = string.Empty;

    }
}
