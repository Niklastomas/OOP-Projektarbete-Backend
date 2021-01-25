using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;
        private readonly IMapper _mapper;
 
        public FriendController(IFriendService friendService,
            IMapper mapper)
        {
            _friendService = friendService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFriends()
        {
            var result = await _friendService.GetFriendsAsync(User.Identity.Name);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var users = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(result.Resource);
            return Ok(users);

        }

        [HttpPost("[action]/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SendFriendRequest(string userId)
        {
            var result = await _friendService.SendFriendRequestAsync(User.Identity.Name, userId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
            
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFriendRequests()
        {
            var result = await _friendService.GetFriendRequestsAsync(User.Identity.Name);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);


        }

        [HttpPut("[action]/{friendRequestId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AcceptFriendRequest(string friendRequestId)
        {
            var result = await _friendService.AcceptFriendRequestAsync(friendRequestId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var newFriend = _mapper.Map<UserDTO>(result.Resource);
            return Ok(newFriend);

        }

        [HttpPut("[action]/{friendRequestId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeclineFriendRequest(string friendRequestId)
        {
            var result = await _friendService.DeclineFriendRequestAsync(friendRequestId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var declinedUser = _mapper.Map<UserDTO>(result.Resource);
            return Ok(declinedUser);

        }
    }
}
