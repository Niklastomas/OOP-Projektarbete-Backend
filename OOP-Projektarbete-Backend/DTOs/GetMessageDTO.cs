using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.DTOs
{
    public class GetMessageDTO
    {
        public string Id { get; set; }
        public string From { get; set; }
        public DateTime? Sent { get; set; }
        public bool Read { get; set; }
        public string MovieId { get; set; }
        public string Message { get; set; }
    }
}
