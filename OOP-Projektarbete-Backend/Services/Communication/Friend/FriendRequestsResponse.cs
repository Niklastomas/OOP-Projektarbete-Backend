using OOP_Projektarbete_Backend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class FriendRequestsResponse : BaseResponse<IEnumerable<FriendRequest>>
    {
        //Success response
        public FriendRequestsResponse(IEnumerable<FriendRequest> requests) : base(requests)
        {

        }

        //Error response
        public FriendRequestsResponse(string message) : base(message)
        {

        }
    }
}
