using System;

namespace AsmvBackend.Models
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessKey { get; set; }

        private readonly string realAccessKey = "beasmv";

        public ChangePassword(string email, string password, string accessKey)
        {
            Email = email;
            Password = password;
            AccessKey = accessKey;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (ChangePassword)obj;
            return string.Equals(AccessKey, realAccessKey, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, Password, AccessKey);
        }
    }
}