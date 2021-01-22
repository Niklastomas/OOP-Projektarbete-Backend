using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Helpers;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Interfaces;
using OOP_Projektarbete_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext context, UserManager<User> userManager, IMovieRepository movieRepository, IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _movieRepository = movieRepository;
            _userService = userService;
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.ListAsync(User.Identity.Name);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
        }



        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMovies()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userService.ListMoviesAsync(user.Id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
        }

        [HttpPost("[action]/{movieId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddMovie(string movieId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userService.AddMovieAsync(movieId, user.Id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
  
        }

        [HttpDelete("[action]/{movieId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteMovie(string movieId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var usersMovies = await _context.UsersMovies.Where(x => x.UserId == user.Id).Where(x => x.MovieId == movieId).FirstOrDefaultAsync();

            if (usersMovies != null)
            {
                _context.UsersMovies.Remove(usersMovies);
                await _context.SaveChangesAsync();
                return Ok(movieId);
            }
            return BadRequest();
        }

        [HttpPost("[action]/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SendFriendRequest(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            var friend = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null && friend != null)
            {
                var friendRequest = new Friend()
                {
                    Id = Guid.NewGuid().ToString(),
                    RequestedBy = user,
                    RequestedTo = friend,
                    RequestedTime = DateTime.Now,
                    FriendRequestFlag = FriendRequestFlag.None
                };
                user.SentFriendRequests.Add(friendRequest);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFriends()
        {
            var friends = new List<UserDTO>();
            
            var user = await _context.Users
                .Include(x => x.SentFriendRequests.Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved))
                .Include(x => x.ReceievedFriendRequests.Where(x => x.FriendRequestFlag == FriendRequestFlag.Approved))
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            if (user.SentFriendRequests != null)
            {
                foreach (var request in user.SentFriendRequests)
                {
                    var friend = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.RequestedToId);
                    var userDTO = new UserDTO()
                    {
                        Id = friend.Id,
                        Username = friend.UserName,
                        Email = friend.Email
                    };
                    friends.Add(userDTO);
                    
                }
            }

            if (user.ReceievedFriendRequests != null)
            {
                foreach (var request in user.ReceievedFriendRequests)
                {
                    var friend = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.RequestedById);
                    var userDTO = new UserDTO()
                    {
                        Id = friend.Id,
                        Username = friend.UserName,
                        Email = friend.Email
                    };
                    friends.Add(userDTO);

                }
            }

            if (friends != null)
                return Ok(friends);

            return BadRequest();
            

        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFriendRequests()
        {
            var friendRequests = new List<FriendRequest>();
            var user = await _context.Users
                .Include(x => x.ReceievedFriendRequests.Where(x => x.FriendRequestFlag == FriendRequestFlag.None))
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            if (user != null)
            {
                foreach (var u in user.ReceievedFriendRequests)
                {
                    var requestSentBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == u.RequestedById);
                    var userDTO = new UserDTO()
                    {
                        Id = requestSentBy.Id,
                        Username = requestSentBy.UserName,
                        Email = requestSentBy.Email
                    };
                    var friendRequest = new FriendRequest()
                    {   Id = u.Id,
                        RequestSentBy = userDTO,
                        RequestedTime = u.RequestedTime
                    };
                    friendRequests.Add(friendRequest);
                }
                return Ok(friendRequests);
            }

            return BadRequest();


        }

        [HttpPost("[action]/{friendRequestId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AcceptFriendRequest(string friendRequestId)
        {
            var user = await _context.Users
                .Include(x => x.ReceievedFriendRequests)
                .ThenInclude(x => x.RequestedBy)
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            var friendRequest = user.ReceievedFriendRequests
                .Where(x => x.Id == friendRequestId)
                .FirstOrDefault();
            friendRequest.FriendRequestFlag = FriendRequestFlag.Approved;
            var newFriend = new UserDTO()
            {
                Id = friendRequest.RequestedBy.Id,
                Email = friendRequest.RequestedBy.Email,
                Username = friendRequest.RequestedBy.UserName
            };
            await _context.SaveChangesAsync();
            return Ok(newFriend);
           
        }

        [HttpPost("[action]/{friendRequestId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeclineFriendRequest(string friendRequestId)
        {
            var user = await _context.Users
                .Include(x => x.ReceievedFriendRequests)
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            var friendRequest = user.ReceievedFriendRequests
                .Where(x => x.Id == friendRequestId)
                .FirstOrDefault();
            friendRequest.FriendRequestFlag = FriendRequestFlag.Rejected;
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task SendMessage([FromBody] SendMessageViewModel model)
        {
            var user = await _context.Users
                .Include(x => x.ReceievedFriendRequests)
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            var userToReceiveMessage = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.SendTo);

            var message = new Message()
            {
                Id = Guid.NewGuid().ToString(),
                MovieId = model.MovieId,
                MessageContent = model.Message,
                Read = false,
                Sent = DateTime.Now,
                SentBy = user.UserName,
                User = userToReceiveMessage
            };

             _context.Messages.Add(message);
            await _context.SaveChangesAsync();


        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMessages()
        {
            var user = await _context.Users
                .Include(x => x.ReceievedFriendRequests)
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            var messages = await _context.Messages.Where(x => x.UserId == user.Id).Select(x => new GetMessageViewModel() {Id = x.Id, From = x.SentBy, Message = x.MessageContent, MovieId = x.MovieId, Sent = x.Sent, Read = x.Read }).ToListAsync();
            if (messages != null)
            {
                return Ok(messages.OrderByDescending(x => x.Sent));
            }
            return NotFound();


        }

        [HttpPost("[action]/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MarkMessageAsRead(string id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (message != null)
            {
                message.Read = true;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound("Message not found");


        }
    }
}
