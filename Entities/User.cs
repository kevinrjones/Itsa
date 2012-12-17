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

        public string Email { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }

        public string Password
        {
            set
            {
                if(string.IsNullOrEmpty(Salt)) throw new ArgumentException("Salt has not been set");
                HashedPassword = GenerateHashedPasswordFromPlaintext(value);
            }
        }

        public string BlogDescription { get; set; }

        public string BlogTitle { get; set; }

        public DateTimeOffset LastUpdated { get; set; }


        private void GeneratePassword(string password)
        {
            SHA256 shaM = new SHA256Managed();
            byte[] data;
            Salt = Convert.ToBase64String(Encoding.UTF32.GetBytes(GetHashCode() + new Random().Next().ToString(CultureInfo.InvariantCulture)));
            data = Encoding.UTF32.GetBytes(password + "wibble" + Salt);
            HashedPassword = Convert.ToBase64String(shaM.ComputeHash(data));
        }

        public bool MatchPassword(string password)
        {
            string hashedPassword = GenerateHashedPasswordFromPlaintext(password);

            return HashedPassword == hashedPassword;
        }

        private string GenerateHashedPasswordFromPlaintext(string password)
        {
            SHA256 shaM = new SHA256Managed();
            byte[] data = Encoding.UTF32.GetBytes(password + "wibble" + Salt);

            return Convert.ToBase64String(shaM.ComputeHash(data));
        }

    }
}