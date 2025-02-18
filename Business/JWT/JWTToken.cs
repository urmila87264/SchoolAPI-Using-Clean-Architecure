using Domain.Authentication;
using Domain.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Appliction.Interfaces.Common;

namespace Infrastructure.JWT
{
    public class JWTToken:IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JWTToken> _logger;

        public JWTToken(IOptions<JwtSettings> jwtOptions, ILogger<JWTToken> logger)
        {
            _jwtSettings = jwtOptions.Value;
            _logger = logger;
        }

        #region JWTToken
        public string GenerateJwtToken(Login login)
        {
            // Validate the login object
            // Validate the login object
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login), "Login object cannot be null");
            }

            if (string.IsNullOrWhiteSpace(login.Email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(login.Email));
            }

            if (login.UserID == 0)  // Check for Guid.Empty, not 0
            {
                throw new ArgumentException("UserID cannot be empty", nameof(login.UserID));
            }

            if (login.RoleId == "0")  // RoleId is an int, so check for 0 (default value)
            {
                throw new ArgumentException("RoleId cannot be zero", nameof(login.RoleId));
            }


            // Log the login details for debugging purposes
            _logger.LogInformation("Generating JWT Token for UserID: {UserID}, Email: {Email}, RoleID: {RoleID}", login.UserID, login.Email, login.RoleId);

            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, login.UserID.ToString()), // Ensure UserID is string
                new Claim(JwtRegisteredClaimNames.UniqueName, login.Email),
                new Claim(ClaimTypes.Role, login.RoleId.ToString()), // Ensure RoleId is string
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Decode the key from Base64 (it is safe because your Key is Base64 encoded)
            byte[] keyBytes;
            try
            {
                keyBytes = Convert.FromBase64String(_jwtSettings.Key); // Decode Base64 key
            }
            catch (FormatException ex)
            {
                _logger.LogError("Error decoding the Base64 key: {Message}", ex.Message);
                throw new ArgumentException("The provided JWT Key is invalid or incorrectly formatted.");
            }

            // Ensure the key length is at least 32 bytes (256 bits) for HMACSHA256
            if (keyBytes.Length < 32)  // 256 bits (32 bytes) is the minimum for HMACSHA256
            {
                _logger.LogError("The key length is insufficient for HMACSHA256. It must be at least 256 bits.");
                throw new ArgumentException("The JWT key must be at least 256 bits (32 bytes) in length.");
            }

            // Create the key from the byte array
            var key = new SymmetricSecurityKey(keyBytes);

            // Signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate the token
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            // Log the generated token (for debugging purposes, not in production)
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("Generated JWT Token: {Token}", jwtToken);

            return jwtToken;
        }
        #endregion
    }
}
