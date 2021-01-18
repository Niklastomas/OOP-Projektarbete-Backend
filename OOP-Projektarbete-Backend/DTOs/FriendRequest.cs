using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.DTOs
{
    public class FriendRequest
    {
        public string Id { get; set; }
        public UserDTO RequestSentBy { get; set; }
        public DateTime? RequestedTime { get; set; }
    }
}
