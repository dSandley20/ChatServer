using System;
namespace ChatServer.Utilities
{
    public class BCryptUtilities
    {
        public static bool verifyPassword(string password, string existingPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, existingPassword);
        }

        public static string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
