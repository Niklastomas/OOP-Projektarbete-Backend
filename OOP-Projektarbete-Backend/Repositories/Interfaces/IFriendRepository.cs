using OOP_Projektarbete_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories
{
    public interface IFriendRepository
    {
        Task<IEnumerable<Friend>> GetRequestsAsync(string userId);
        Task<Friend> GetByIdAsync(string id);
        Task<IEnumerable<Friend>> GetReceivedApprovedAsync(string userId);
        Task<IEnumerable<Friend>> GetSentApprovedAsync(string userId);
        Task AddAsync(Friend friend);
    }
}