using System.Collections.Generic;

using basic_banking_app_server.Models;

namespace basic_banking_app_server.Data.UserRepo
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);

        bool SaveChanges();
    }
}
