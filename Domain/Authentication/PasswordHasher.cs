using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication
{
    public static class PasswordHasher
    {
        public static string GeneratePassword(string password)
        {
            // Generate a 128-bit salt (16 bytes)
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt using PBKDF2
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000, // Number of iterations for hashing
                numBytesRequested: 256 / 8 // Hash size (32 bytes)
            ));

            // Combine the salt and hash for storage (salt.base64 + "." + hash)
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            // Split the stored value to get the salt and hashed password
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2)
            {
                return false; // Invalid stored hash format
            }

            // Extract the salt and stored hash
            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            // Hash the provided password with the extracted salt
            string providedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            // Compare the stored hash with the newly generated hash
            return storedHash == providedHash;
        }

    }
}

