using Microsoft.EntityFrameworkCore;
using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories
{
    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public FriendRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task AddAsync(Friend friend)
        {
            await _context.AddAsync(friend);
        }

        public async Task<IEnumerable<Friend>> GetRequestsAsync(string userId)
        {
            return await _context.Friends.Where(x => x.RequestedToId == userId)
                .Where(x => x.FriendRequestFlag == FriendRequestFlag.None).ToListAsync();
        }

        public async Task<IEnumerable<Friend>> GetReceivedApprovedAsync(string userId)
        {
            return await _context.Friends.Where(x => x.RequestedToId == userId)
                 .Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved).ToListAsync();
        }

        public async Task<IEnumerable<Friend>> GetSentApprovedAsync(string userId)
        {
            return await _context.Friends.Where(x => x.RequestedById == userId)
                 .Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved).ToListAsync();
        }

        public Task<Friend> GetByIdAsync(string id)
        {
            return _context.Friends.Include(x => x.RequestedBy).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
