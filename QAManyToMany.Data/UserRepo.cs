using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAManyToMany.Data
{
    public class UserRepo
    {
        private string _connectionString;
        public UserRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            using var context = new QADbContext(_connectionString);
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(password);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User LogIn(string email, string password)
        {
          User user = GetUserByEmail(email);
            if(user == null)
            {
                return null;
            }
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.HashPassword);
            if (isValidPassword)
            {
                return user;
            }
            return null;
        }
        public User GetUserByEmail(string email)
        {
            using var context = new QADbContext(_connectionString);

            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
