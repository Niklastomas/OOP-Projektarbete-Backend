using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Communication.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessagesResponse> GetMessagesAsync(string email);
        Task<MessageResponse> MarkMessageAsRead(string messageId);
        Task<MessageResponse> SendMessageAsync(SendMessageDTO message);
    }
}
