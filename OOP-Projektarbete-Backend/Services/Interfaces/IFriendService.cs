using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Interfaces
{
    public interface IFriendService
    {
        Task<FriendsResponse> GetFriendsAsync(string email);
        Task<FriendResponse> SendFriendRequestAsync(string requestedByEmail, string requestedToId);
        Task<FriendRequestsResponse> GetFriendRequestsAsync(string email);
        Task<UserResponse> AcceptFriendRequestAsync(string requestId);
        Task<UserResponse> DeclineFriendRequestAsync(string requestId);
    }
}
