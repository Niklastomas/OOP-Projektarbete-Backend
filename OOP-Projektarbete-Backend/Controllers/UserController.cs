﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMovieRepository _movieRepository;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IMovieRepository movieRepository)
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
        public async Task AddMovie(string movieId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var usersMovies = new UsersMovies()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                MovieId = movieId
            };
            _context.UsersMovies.Add(usersMovies);
            await _context.SaveChangesAsync();
        }
    }
}