using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using OOP_Projektarbete_Backend.Services.Communication;
using OOP_Projektarbete_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services
{
    public class FriendService : IFriendService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FriendService(IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FriendRequestResponse> SendFriendRequestAsync(string requestedByEmail, string requestedToId)
        {
            try
            {
                var requestedBy = await _userRepository.GetUserByEmailAsync(requestedByEmail);
                var requestedTo = await _userRepository.GetUserByIdAsync(requestedToId);
                if (requestedBy != null && requestedTo != null)
                {
                    var friendRequest = new Friend()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RequestedBy = requestedBy,
                        RequestedTo = requestedTo,
                        RequestedTime = DateTime.Now,
                        FriendRequestFlag = FriendRequestFlag.None
                    };
                    requestedBy.SentFriendRequests.Add(friendRequest);
                    await _unitOfWork.CompleteAsync();
                    return new FriendRequestResponse(friendRequest);
                }
                return new FriendRequestResponse("User not found");
            }
            catch (Exception ex)
            {
                return new FriendRequestResponse(ex.Message);
            }
            
        }

        public async Task<FriendsResponse> GetFriendsAsync(string email)
        {
            try
            {
                var friends = new List<User>();
                var user = await _userRepository.GetUserWithFriendsAsync(email);
                if (user.SentFriendRequests != null)
                {
                    foreach (var item in user.SentFriendRequests)
                    {
                        var friend = await _userRepository.GetUserByIdAsync(item.RequestedToId);
                        friends.Add(friend);
                    }                 
                }
                if (user.ReceievedFriendRequests != null)
                {
                    foreach (var item in user.ReceievedFriendRequests)
                    {
                        var friend = await _userRepository.GetUserByIdAsync(item.RequestedById);
                        friends.Add(friend);
                    }
                }
                if (friends.Count == 0)
                {
                    return new FriendsResponse("No friends found");
                }
                return new FriendsResponse(friends);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
