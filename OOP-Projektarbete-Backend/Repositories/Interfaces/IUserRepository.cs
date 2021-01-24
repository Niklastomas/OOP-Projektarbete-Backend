using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAllAsync(string email);
        Task<User> GetUserWithFriendsAsync(string email);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
    }
}
