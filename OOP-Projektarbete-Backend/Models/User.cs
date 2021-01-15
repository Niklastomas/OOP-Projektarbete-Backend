using Microsoft.AspNetCore.Identity;
using OOP_Projektarbete_Backend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Models
{
    public class User : IdentityUser
    {
        public List<Friend> SentFriendRequests { get; set; } = new List<Friend>();

        public List<Friend> ReceievedFriendRequests { get; set; } = new List<Friend>();

        public List<UserDTO> Friends { get; set; } = new List<UserDTO>();
        //{
        //    get
        //    {
        //        var friends = SentFriendRequests.Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved).ToList();
        //        friends.AddRange(ReceievedFriendRequests.Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved));
        //        return friends;
        //    }
        //}
    }
}