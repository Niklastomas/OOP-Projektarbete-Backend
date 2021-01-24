using Microsoft.EntityFrameworkCore;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

      

        public async Task<IEnumerable<User>> GetAllAsync(string email)
        {
            return await _context.Users.Where(x => x.Email != email).ToListAsync();

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserWithFriendsAsync(string email)
        {
            return await _context.Users
                .Include(x => x.SentFriendRequests.Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved))
                .Include(x => x.ReceievedFriendRequests.Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved))
                .FirstOrDefaultAsync(x => x.Email == email);
           
        }

    }
}
