using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using OOP_Projektarbete_Backend.Services.Communication.Messages;
using OOP_Projektarbete_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public MessageService(IMessageRepository messageRepository,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<MessagesResponse> GetMessagesAsync(string email)
        {         
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                var messages = await _messageRepository.GetMessagesAsync(user.Id);
                if (messages != null)
                {
                    return new MessagesResponse(messages);
                }
                return new MessagesResponse("No messages found");               
            }
            catch (Exception ex)
            {
                return new MessagesResponse(ex.Message);
            }
            
        }

        public async Task<MessageResponse> SendMessageAsync(SendMessageDTO message)
        {
            try
            {
                var userToReceiveMessage = await _userRepository.GetUserByEmailAsync(message.SendTo);
                var newMessage = new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    MovieId = message.MovieId,
                    MessageContent = message.Message,
                    Read = false,
                    Sent = DateTime.Now,
                    SentBy = message.SentBy,
                    User = userToReceiveMessage
                };
                await _messageRepository.AddAsync(newMessage);
                await _unitOfWork.CompleteAsync();
                return new MessageResponse(newMessage);
            }
            catch (Exception ex)
            {
                return new MessageResponse(ex.Message);
            }
            
        }

        public async Task<MessageResponse> MarkMessageAsRead(string messageId)
        {
            try
            {
                var message = await _messageRepository.GetMessageByIdAsync(messageId);
                if (message != null)
                {
                    message.Read = true;
                    await _unitOfWork.CompleteAsync();
                    return new MessageResponse(message);
                }
                return new MessageResponse("No message found");
            }
            catch (Exception ex)
            {
                return new MessageResponse(ex.Message);
            }

        }
    }
}
