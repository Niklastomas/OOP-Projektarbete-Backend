using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.DTOs
{
    public class UserToken
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public DateTime Expiration { get; set; }
    }
}