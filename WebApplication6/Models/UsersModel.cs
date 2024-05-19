using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebApplication6.Helpers;
using System.Data.SqlClient;

namespace WebApplication6.Models
{
    public class Users
    {
        private readonly Database _database;

        public Users()
        {
            _database = new Database();
        }

        public DataTable GetUsers()
        {
            string sql = "SELECT * FROM Users";
            return _database.Query(sql);
        }

        public int AddUser(string email, string password)
        {
            string sql = "INSERT INTO users (email, password) VALUES (@Email, @Password)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };
            return _database.Execute(sql, parameters);
        }

        public int RemoveUser(int UserId)
        {
            string sql = "DELETE FROM users WHERE UserId = @UserId";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", UserId),
            };
            return _database.Execute(sql, parameters);
        }

        public DataTable GetUser(int UserId = 0)
        {
            string sql = "SELECT * FROM users WHERE UserId = @UserId";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", UserId)
            };

            return _database.Query(sql, parameters);
        }

        public int UpdateUser(int UserId, string Email, string Password)
        {
            string sql = "UPDATE users SET Email = @Email, Password = @Password WHERE UserId = @UserId";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", Email),
                new SqlParameter("@Password", Password),
                new SqlParameter("@UserId", UserId),
            };

            return _database.Execute(sql, parameters);
        }

        public DataTable Auth(string email, string password)
        {
            string sql = "SELECT * FROM users WHERE Email = @email AND Password = @password";

            var parameteres = new List<SqlParameter>
            {
                 new SqlParameter("@email", email),
                 new SqlParameter("@password", password)
            };

            return _database.Query(sql, parameteres);
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}