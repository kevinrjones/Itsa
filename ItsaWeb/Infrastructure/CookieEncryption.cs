using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ItsaWeb.Infrastructure
{
    public static class CookieEncryption
    {
        private static Rfc2898DeriveBytes KeyGenerator;

        private static byte[] _keyValue;
        private static byte[] _ivValue;

        private static byte[] Key
        {
            get
            {
                if (_keyValue == null)
                    _keyValue = KeyGenerator.GetBytes(16);
                return _keyValue;
            }
        }

        private static byte[] InitializationVector
        {
            get
            {
                if (_ivValue == null)
                    _ivValue = KeyGenerator.GetBytes(16);
                return _ivValue;
            }
        }

        public static byte[] Encrypt(this string plainText, string salt)
        {
            var saltBytes = Encoding.Default.GetBytes(salt);
            KeyGenerator = new Rfc2898DeriveBytes(ConfigurationManager.AppSettings["keyphrase"], saltBytes);

            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");

            byte[] encrypted;

            using (var aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = InitializationVector;

                ICryptoTransform transform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                encrypted = Encrypt(plainText, transform);
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static string Decrypt(this byte[] cipherText, string salt)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException("cipherText");

            var saltBytes = Encoding.Default.GetBytes(salt); 
            KeyGenerator = new Rfc2898DeriveBytes(ConfigurationManager.AppSettings["keyphrase"], saltBytes);

            string plaintext;

            using (var aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = InitializationVector;

                ICryptoTransform transform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                plaintext = Decrypt(cipherText, transform);
            }
            return plaintext;
        }

        private static byte[] Encrypt(string plainText, ICryptoTransform transform)
        {
            byte[] encrypted;
            // Create the streams used for encryption.
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, transform, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                        swEncrypt.Flush();
                    }
                }
                encrypted = msEncrypt.ToArray();
            }
            return encrypted;
        }

        private static string Decrypt(byte[] cipherText, ICryptoTransform transform)
        {
            string plaintext;
            using (var msDecrypt = new MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, transform, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}