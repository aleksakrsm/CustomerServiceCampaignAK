using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.DTOs
{
    public record RequestOtpDto
    {
        [Required]
        [MaxLength(500)]
        public string Message { get; init; } = string.Empty;
    }
}
