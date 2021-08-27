using System.Collections.Generic;

using basic_banking_app_server.Models;

namespace basic_banking_app_server.Data.UserRepo
{
    public interface IUserRepo
    {
        bool IsEmailUsed(string email);
        //string HashPassword(string password);
        //bool VerifyHashedPassword(string hashedPassword, string password);
        User Authenticate(string email, string password);
        string GenerateJwtToken(User user);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        bool SaveChanges();
    }
}
