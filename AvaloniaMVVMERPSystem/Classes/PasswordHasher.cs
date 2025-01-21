using System;
using System.Security.Cryptography;
using System.Text;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class PasswordHasher
    {
        private const int SaltSize = 16; // 128-bit salt
        private const int KeySize = 32;  // 256-bit key
        private const int Iterations = 10000; // Number of PBKDF2 iterations

        public static string HashPassword(string password)
        {
#pragma warning disable
            // Generate a salt
            using var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Hash the password with PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);

            // Combine salt and key for the final hash
            byte[] hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(key, 0, hashBytes, SaltSize, KeySize);

            // Convert to a base64 string
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt from the hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Extract the key from the hash
            byte[] storedKey = new byte[KeySize];
            Array.Copy(hashBytes, SaltSize, storedKey, 0, KeySize);

            // Hash the input password with the same salt and settings
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);

            // Compare the results
            return CryptographicOperations.FixedTimeEquals(storedKey, key);
        }
    }

}
