using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication.Messages
{
    public class MessagesResponse : BaseResponse<IEnumerable<Message>>
    {
        public MessagesResponse(IEnumerable<Message> messages) : base(messages)
        {

        }

        public MessagesResponse(string message) : base(message)
        {

        }
    }
}
