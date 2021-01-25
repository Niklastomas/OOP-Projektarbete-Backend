using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class FriendsResponse : BaseResponse<IEnumerable<User>>
    {
        //Success response
        public FriendsResponse(IEnumerable<User> friends) : base(friends)
        {

        }

        //Error response
        public FriendsResponse(string message) : base(message)
        {

        }
    }
}
