using AutoMapper;
using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories;
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
        private readonly IFriendRepository _friendRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FriendService(IUserRepository userRepository,
            IFriendRepository friendRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FriendResponse> SendFriendRequestAsync(string requestedByEmail, string requestedToId)
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
                    await _friendRepository.AddAsync(friendRequest);
                    await _unitOfWork.CompleteAsync();
                    return new FriendResponse(friendRequest);
                }
                return new FriendResponse("User not found");
            }
            catch (Exception ex)
            {
                return new FriendResponse(ex.Message);
            }
            
        }

        public async Task<FriendsResponse> GetFriendsAsync(string email)
        {
            try
            {
                var friends = new List<User>();
                var user = await _userRepository.GetUserWithFriendsAsync(email);

                var received = await _friendRepository.GetReceivedApprovedAsync(user.Id);
                var sent = await _friendRepository.GetSentApprovedAsync(user.Id);

                if (received != null)
                {
                    foreach (var request in received)
                    {
                        var friend = await _userRepository.GetUserByIdAsync(request.RequestedById);
                        friends.Add(friend);
                    }
                }
                if (sent != null)
                {
                    foreach (var request in sent)
                    {
                        var friend = await _userRepository.GetUserByIdAsync(request.RequestedToId);
                        friends.Add(friend);
                    }
                }
                return new FriendsResponse(friends);
           
            }
            catch (Exception ex)
            {
                return new FriendsResponse(ex.Message);
            }
        }

        public async Task<FriendRequestsResponse> GetFriendRequestsAsync(string email)
        {
            try
            {
                var friendRequests = new List<FriendRequest>();
                var user = await _userRepository.GetUserByEmailAsync(email);
                var requests = await _friendRepository.GetRequestsAsync(user.Id);
                if (requests != null)
                {
                    foreach (var request in requests)
                    {
                        var requestSentBy = _mapper.Map<UserDTO>(await _userRepository.GetUserByIdAsync(request.RequestedById));
                        var friendRequest = new FriendRequest()
                        {
                            Id = request.Id,
                            RequestSentBy = requestSentBy,
                            RequestedTime = request.RequestedTime
                        };
                        friendRequests.Add(friendRequest);
                    }
                    return new FriendRequestsResponse(friendRequests);
                }
                return new FriendRequestsResponse("No requests found");
            }
            catch (Exception ex)
            {
                return new FriendRequestsResponse(ex.Message);
            }            
        }

        public async Task<UserResponse> AcceptFriendRequestAsync(string requestId)
        {
            try
            {
                var friendRequest = await _friendRepository.GetByIdAsync(requestId);
                if (friendRequest != null)
                {
                    friendRequest.FriendRequestFlag = FriendRequestFlag.Approved;
                    await _unitOfWork.CompleteAsync();
                    return new UserResponse(friendRequest.RequestedBy);
                }
                return new UserResponse("No requests found");
            }
            catch (Exception ex)
            {
                return new UserResponse(ex.Message);
            }
            
            
        }

        public async Task<UserResponse> DeclineFriendRequestAsync(string requestId)
        {
            try
            {
                var friendRequest = await _friendRepository.GetByIdAsync(requestId);
                if (friendRequest != null)
                {
                    friendRequest.FriendRequestFlag = FriendRequestFlag.Rejected;
                    await _unitOfWork.CompleteAsync();
                    return new UserResponse(friendRequest.RequestedBy);
                }
                return new UserResponse("No requests found");
            }
            catch (Exception ex)
            {
                return new UserResponse(ex.Message);
            }


        }
    }
}
