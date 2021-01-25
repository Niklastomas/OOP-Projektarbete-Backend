using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task<Message> GetMessageByIdAsync(string messageId);
        Task<IEnumerable<Message>> GetMessagesAsync(string userId);
    }
}
