using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class UserResponse : BaseResponse<IEnumerable<User>>
    {
        //Success response
        public UserResponse(IEnumerable<User> users) : base(users)
        {

        }

        //Error response
        public UserResponse(string message) : base(message)
        {

        }
    }
}
