using System.Security.Cryptography;
using System.Text;

namespace CebuFitApi.Helpers
{
    public static class PasswordGenerator
    {
        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder password = new StringBuilder();
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] buffer = new byte[1];

                while (password.Length < length)
                {
                    rng.GetBytes(buffer);
                    char randomChar = (char)buffer[0];

                    if (validChars.IndexOf(randomChar) != -1)
                    {
                        password.Append(randomChar);
                    }
                }
            }

            return password.ToString();
        }
    }
}
