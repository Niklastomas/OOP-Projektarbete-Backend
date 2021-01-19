using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.ViewModels
{
    public class MessageViewModel
    {
        public string SendTo { get; set; }
        public string From { get; set; }
        public string MovieId { get; set; }
        public string Message { get; set; }
    }
}
