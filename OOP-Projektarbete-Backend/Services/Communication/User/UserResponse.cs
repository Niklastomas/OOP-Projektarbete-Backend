using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        //Success response
        public UserResponse(User user) : base(user)
        {

        }

        //Error response
        public UserResponse(string message) : base(message)
        {

        }
    }
}
