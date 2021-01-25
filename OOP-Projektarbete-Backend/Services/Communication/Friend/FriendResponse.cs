using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class FriendResponse : BaseResponse<Friend>
    {
        //Success response
        public FriendResponse(Friend friend) : base(friend)
        {

        }

        //Error response
        public FriendResponse(string message) : base(message)
        {
            
        }
    }
}
