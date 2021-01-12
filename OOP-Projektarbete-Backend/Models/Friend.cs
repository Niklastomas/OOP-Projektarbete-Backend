using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Models
{
    public class Friend
    {
        public string Id { get; set; }
        public string RequestedById { get; set; }
        public string RequestedToId { get; set; }
        public User RequestedBy { get; set; }
        public User RequestedTo { get; set; }
        public DateTime? RequestedTime { get; set; }
        public FriendRequestFlag FriendRequestFlag { get; set; }
    }

    public enum FriendRequestFlag
    {
        None,
        Approved,
        Rejected
    };
}