using Microsoft.EntityFrameworkCore;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<Message> GetMessageByIdAsync(string messageId)
        {
            return await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string userId)
        {
            return await _context.Messages.Where(x => x.UserId == userId).OrderByDescending(x => x.Sent).ToListAsync();
        }
    }
}
