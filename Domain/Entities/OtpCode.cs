using Domain.Base;
using Domain.Exceptions;
using Domain.Guards;
using System.Security.Cryptography;

namespace Domain.Entities
{
    public enum OtpVerificationResult
    {
        Success,
        InvalidCode,
        Expired,
        AlreadyUsed,
    }

    public class OtpCode : Entity
    {
        public const int DefaultOtpLength = 8;
        private OtpCode()
        {
        }
        public string Email { get; private set; } = null!;
        public string Code { get; private set; } = null!;
        public DateTime ExpiresAt { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime? UsedAt { get; private set; }

        public static OtpCode Create(
            string email,
            int otpLength = DefaultOtpLength,
            int expirationMinutes = 60)
        {
            Guard.Against.NullOrWhiteSpace(email, "Email is required");
            Guard.Against.Negative(expirationMinutes - 1, "Expiration minutes must be positive");

            var otp = new OtpCode
            {
                Email = email.Trim().ToLowerInvariant(),
                Code = GenerateSecureCode(otpLength),
                ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
                IsUsed = false
            };

            return otp;
        }

        public bool IsValid()
        {
            return !IsUsed && DateTime.UtcNow <= ExpiresAt;
        }

        public OtpVerificationResult VerifyAndUse(string code)
        {
            if (IsUsed)
            {
                return OtpVerificationResult.AlreadyUsed;
            }

            if (DateTime.UtcNow > ExpiresAt)
            {
                return OtpVerificationResult.Expired;
            }

            MarkAsUsed();

            return OtpVerificationResult.Success;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
            UsedAt = DateTime.UtcNow;
        }
        private static string GenerateSecureCode(int length)
        {
            const int maxIterations = 100;

            var minValue = (int)Math.Pow(10, length - 1);
            var maxValue = (int)Math.Pow(10, length);
            var range = (uint)(maxValue - minValue);

            var maxAcceptable = uint.MaxValue - (uint.MaxValue % range);

            Span<byte> bytes = stackalloc byte[4];
            uint randomValue;
            var iterations = 0;

            do
            {
                if (++iterations > maxIterations)
                {
                    throw new DomainException($"Failed to generate secure OTP code after {maxIterations} attempts. This indicates a potential issue with the random number generator.");
                }

                RandomNumberGenerator.Fill(bytes);
                randomValue = BitConverter.ToUInt32(bytes);
            }
            while (randomValue >= maxAcceptable);

            var result = minValue + (int)(randomValue % range);
            return result.ToString();
        }
    }
}
