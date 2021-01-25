using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public MessageController(IMessageService messageService,
            IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMessages()
        {

            var result = await _messageService.GetMessagesAsync(User.Identity.Name);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var messages = _mapper.Map<IEnumerable<Message>, IEnumerable<GetMessageDTO>>(result.Resource);
            return Ok(messages);

        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO message)
        {         
            var result = await _messageService.SendMessageAsync(message);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPut("[action]/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MarkMessageAsRead(string id)
        {
            var result = await _messageService.MarkMessageAsRead(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok();


        }
    }
}
