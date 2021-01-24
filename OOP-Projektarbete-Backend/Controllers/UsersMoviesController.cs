using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class UsersMoviesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUsersMoviesService _usersMoviesService;
        public UsersMoviesController(UserManager<User> userManager,
            IUsersMoviesService usersMoviesService)
        {
            _userManager = userManager;
            _usersMoviesService = usersMoviesService;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMovies()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _usersMoviesService.GetAllAsync(user.Id);

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
            var result = await _usersMoviesService.AddAsync(movieId, user.Id);

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
            var result = await _usersMoviesService.DeleteAsync(movieId, user.Id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
        }

    }
}
