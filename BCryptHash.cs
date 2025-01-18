namespace TradingSystemApi
{
    public class BCryptHash
    {
        public string HashPassword(string password)
        {
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashPassword;
        }

        public bool VerifyPassword(string password, string hashPassword)
        {
            bool isCorrect = BCrypt.Net.BCrypt.Verify(password, hashPassword);

            return isCorrect;
        }
    }
}
