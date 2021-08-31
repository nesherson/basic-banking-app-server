using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using basic_banking_app_server.Data.Context;
using basic_banking_app_server.Models.UserModel;


namespace basic_banking_app_server.Data.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly BasicBankContext _context = null;
        private readonly IConfiguration _configuration;

        public UserRepo(BasicBankContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public bool IsEmailUsed(string email)
        {
            var emailItem = _context.Users.FirstOrDefault(user => user.Email == email);

            if (emailItem == null)
            {
                return false;
            }

            return true;
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return buffer3.SequenceEqual(buffer4);
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            if (!VerifyHashedPassword(user.Password, password))
                return null;

            return user;
        }

        public string GenerateJwtToken(User user)
        {
            string secretKey = _configuration["SecretKey"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
               }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
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

            string hashedPassword = HashPassword(user.Password);
            user.Password = hashedPassword;

            _context.Users.Add(user);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
