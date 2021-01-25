using OOP_Projektarbete_Backend.Models;
using System.Collections.Generic;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class UsersResponse : BaseResponse<IEnumerable<User>>
    {
        //Success response
        public UsersResponse(IEnumerable<User> users) : base(users)
        {

        }

        //Error response
        public UsersResponse(string message) : base(message)
        {

        }
    }
}
