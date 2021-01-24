using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class FriendRequestResponse : BaseResponse<Friend>
    {
        public FriendRequestResponse(Friend friend) : base(friend)
        {

        }

        public FriendRequestResponse(string message) : base(message)
        {
            
        }
    }
}
