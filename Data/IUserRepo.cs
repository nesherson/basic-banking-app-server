using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using basic_banking_app_server.Models;

namespace basic_banking_app_server.Data
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
    }
}
