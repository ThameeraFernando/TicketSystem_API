
using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TicketSystem_API.Models;
using System.Security.Cryptography;
using System.Text;

namespace TicketSystem_API.services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoClient mongoClient, IUserStoreDatabaseSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task<User> RegisterUser(User newUser)
        {
            // Hash the password before storing it in the database
            newUser.password = HashPassword(newUser.password);

            // Check if a user with the same NIC already exists
            var existingUser = await _users.Find(u => u.nic == newUser.nic).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                // NIC is already taken; you can throw an exception or return an error message
                throw new InvalidOperationException("NIC already exists.");
            }

            await _users.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _users.Find(u => u.email == email).FirstOrDefaultAsync();

            if (user != null && VerifyPassword(password, user.password))
            {
                // Password is correct; return the user
                return user;
            }

            // Authentication failed
            return null;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return HashPassword(inputPassword) == hashedPassword;
        }

        public async Task<User> UpdateUser(string nic, User updatedUser)
        {
            // Check if the user exists
            var existingUser = await _users.Find(u => u.nic == nic).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found");
            }

            // Update user properties
            existingUser.fullName = updatedUser.fullName;
            existingUser.email= updatedUser.email;
            existingUser.type = updatedUser.type;

            // Update other properties as needed

            await _users.ReplaceOneAsync(u => u.nic == nic, existingUser);

            return existingUser;
        }

        public async Task<bool> DeleteUser(string nic)
        {
            var result = await _users.DeleteOneAsync(u => u.nic == nic);

            return result.DeletedCount > 0;
        }
        public async Task<User> UpdateUserIsActive(string nic, bool isActive)
        {
            // Check if the user exists
            var existingUser = await _users.Find(u => u.nic == nic).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found");
            }

            // Update the isActive property
            existingUser.isActivate = isActive;

            await _users.ReplaceOneAsync(u => u.nic == nic, existingUser);

            return existingUser;
        }
    }
}