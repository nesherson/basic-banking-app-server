using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using basic_banking_app_server.Models.UserModel;

namespace basic_banking_app_server.Data.UserRepo
{
    public class MockUserRepo : IUserRepo
    {

        private List<User> users = new List<User>
            {
                new User { Id = 0, Email = "neero@test.com", FirstName = "Neero", LastName = "Tantrum", Password = "123neero" },
                new User { Id = 1, Email = "adam@test.com", FirstName = "Adam", LastName = "Adamson", Password = "123adam" },
                new User { Id = 2, Email = "daniel@test.com", FirstName = "Daniel", LastName = "Damner", Password = "123daniel" },
                new User { Id = 3, Email = "tim@test.com", FirstName = "Tim", LastName = "Goliath", Password = "123tim" }

            };

        public User Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        public User GetUserById(int id)
        {
            var user = users.Find(user => user.Id == id);

            return user;
        }

        public bool IsEmailUsed(string email)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
