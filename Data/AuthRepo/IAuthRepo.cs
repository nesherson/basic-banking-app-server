using basic_banking_app_server.Models;

namespace basic_banking_app_server.Data.AuthRepo
{
    public interface IAuthRepo
    {
        bool IsEmailUsed(string email);
        string HashPassword(string password);
        bool verifyHashedPassword(string hashedPassword, string password);
    }
}
