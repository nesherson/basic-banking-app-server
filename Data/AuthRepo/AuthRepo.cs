﻿using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

using basic_banking_app_server.Data.Context;
using basic_banking_app_server.Models;

namespace basic_banking_app_server.Data.AuthRepo
{
    public class AuthRepo : IAuthRepo
    {
        private readonly BasicBankContext _context = null;
        //private readonly PasswordHasher<User> _passwordHasher = null;

        public AuthRepo(BasicBankContext context)
        {
            _context = context;
            //_passwordHasher = passwordHasher;
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

        public string HashPassword(string password)
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

        public bool verifyHashedPassword(string hashedPassword, string password)
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
    }
}
