﻿using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication.Messages
{
    public class MessageResponse : BaseResponse<Message>
    {
        public MessageResponse(Message message) : base(message)
        {

        }
        public MessageResponse(string message) : base(message)
        {

        }
    }   
        
}
