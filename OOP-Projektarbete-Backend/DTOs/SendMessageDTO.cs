using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.DTOs
{
    public class SendMessageDTO
    {
        public string SentBy { get; set; }
        public string SendTo { get; set; }
        public DateTime? Sent { get; set; }

        public string MovieId { get; set; }
        public string Message { get; set; }
    }
}
