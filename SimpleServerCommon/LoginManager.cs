using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleServerCommon
{
    public class LoginManager
    {
        private static int nextUserId = 100;

        private static Dictionary<string, User> usersDictionary = new Dictionary<string, User>();

        public static void GetUsers(List<User> usersList)
        {
            foreach (var user in usersList)
            {
                usersDictionary[user.UserName] = user;
            }
        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }


        public static bool AuthenticateUser(string username, string password)
        {

            if (usersDictionary.TryGetValue(username, out User user))
            {
                string storedSalt = user.Salt;
                string storedHashedPassword = user.HashedPassword;

                string hashedPassword = GenerateHashedPassword(password, storedSalt);

                return string.Equals(storedHashedPassword, hashedPassword);
            }

            return false;
        }

        public static string GenerateHashedPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(salt);

                byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
                passwordBytes.CopyTo(saltedPassword, 0);
                saltBytes.CopyTo(saltedPassword, passwordBytes.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                return Convert.ToBase64String(hashedBytes);
            }
        }

        private static string ConvertToHex(byte[] hashedBytes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashedBytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public static bool ValidateAuthToken(string authToken)
        {
            return !string.IsNullOrEmpty(authToken);
        }

        public static int GetNextUserId()
        {
            return Interlocked.Increment(ref nextUserId);
        }

        public static string GenerateAuthToken(string username)
        {
            string token = $"{username}: {Guid.NewGuid()}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        }
    }
}
