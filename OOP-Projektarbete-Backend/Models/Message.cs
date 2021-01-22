using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string MovieId { get; set; }
        public string MessageContent { get; set; }
        public bool Read { get; set; }
        public DateTime? Sent { get; set; }
        public string SentBy { get; set; }


        public User User { get; set; }
        public string UserId { get; set; }
       
    }
}
