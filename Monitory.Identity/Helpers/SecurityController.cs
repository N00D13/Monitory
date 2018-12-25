using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Monitory.Identity.Helpers
{
    public class Security
    {
        // Properties
        private static string saltString = "mM2442iab*.4";

        // Password Hashing Function
        public static byte[] GenerateSaltedHash(string password)
        {

            byte[] plainText = Encoding.Unicode.GetBytes(password);
            byte[] salt = Encoding.Unicode.GetBytes(saltString);

            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
    }
}
