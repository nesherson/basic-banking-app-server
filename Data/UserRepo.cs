using basic_banking_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace basic_banking_app_server.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly BasicBankContext _context = null;

        public UserRepo(BasicBankContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _context.Users.ToList();

            return users;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == id);

            return user;
        }

        public void CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
