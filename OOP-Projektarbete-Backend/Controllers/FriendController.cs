using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Interfaces;
using OOP_Projektarbete_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var users = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result.Resource);
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
    }
}
