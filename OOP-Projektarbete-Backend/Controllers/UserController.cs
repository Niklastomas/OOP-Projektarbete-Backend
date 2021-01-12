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

        public UserController(ApplicationDbContext context, UserManager<User> userManager, IMovieRepository movieRepository)
        {
            _context = context;
            _userManager = userManager;
            _movieRepository = movieRepository;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMovies()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var usersMovies = await _context.UsersMovies.Where(x => x.UserId == user.Id).ToListAsync();
            var movies = new List<Movie>();

            foreach (var item in usersMovies)
            {
                var movie = await _movieRepository.GetMovieDetails(item.MovieId);
                if (movie != null)
                {
                    movies.Add(movie);
                }
            }

            if (movies != null)
            {
                return Ok(movies);
            }
            return BadRequest();
        }

        [HttpPost("[action]/{movieId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddMovie(string movieId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var movie = await _movieRepository.GetMovieDetails(movieId);
            var usersMovies = new UsersMovies()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                MovieId = movieId
            };
            _context.UsersMovies.Add(usersMovies);
            await _context.SaveChangesAsync();
            return Ok(movie);
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
            var user = await _context.Users.Include(x => x.SentFriendRequests).FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            return Ok(user);
        }
    }
}