using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Entities
{
    public class User
    {
        public User()
        {
            
        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            GeneratePassword(password);
        }

        protected string Email { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string Password
        {
            set { HashedPassword = GenerateHashedPasswordFromPlaintext(value); }
        }


        private void GeneratePassword(string password)
        {
            byte[] data;
            Salt = Convert.ToBase64String(Encoding.UTF32.GetBytes(GetHashCode() + new Random().Next().ToString(CultureInfo.InvariantCulture)));
            HashedPassword = GenerateHashedPasswordFromPlaintext(password);
        }

        public bool MatchPassword(string password)
        {
            return HashedPassword == GenerateHashedPasswordFromPlaintext(password);
        }

        private string GenerateHashedPasswordFromPlaintext(string password)
        {
            SHA256 sha256 = new SHA256Managed();
            byte[] data = Encoding.UTF32.GetBytes(password + "wibble" + Salt);

            return Convert.ToBase64String(sha256.ComputeHash(data));
        }

    }
}