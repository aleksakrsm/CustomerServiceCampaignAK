using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions.Configurations
{
    public class JwtOptions
    {
        public const string SectionName = "JWT";

        [Required(ErrorMessage = "JWT SecretKey is required")]
        [MinLength(32, ErrorMessage = "JWT SecretKey must be at least 32 characters")]
        public string SecretKey { get; set; } = string.Empty;

        [Required]
        public string Issuer { get; set; } = "CampaignAPI";

        [Range(1, 1440)]
        public int AccessTokenExpirationMinutes { get; set; } = 60;
    }
}
