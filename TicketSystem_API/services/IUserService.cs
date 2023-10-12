using System.Threading.Tasks;
using TicketSystem_API.Models;

namespace TicketSystem_API.services
{
    public interface IUserService
    {
        Task<User> RegisterUser(User newUser);
        Task<User> AuthenticateUser(string username, string password);
        Task<User> UpdateUser(string id, User updatedUser);
        Task<bool> DeleteUser(string id);

        Task<User> UpdateUserIsActive(string id, bool isActive);
    }
}