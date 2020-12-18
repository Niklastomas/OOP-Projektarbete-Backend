using Microsoft.AspNetCore.Mvc;
using OOP_Projektarbete_Backend.Helpers;
using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OOP_Projektarbete_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET: api/<MovieController>/Trending
        [HttpGet("[action]")]
        public async Task<IActionResult> Trending()
        {
            var movies = await _movieRepository.GetTrendingMovies();

            if (movies != null)
                return Ok(movies);

            return BadRequest();
        }

        // GET: api/<MovieController>/Popular
        [HttpGet("[action]")]
        public async Task<IActionResult> Popular()
        {
            var movies = await _movieRepository.GetPopularMovies();

            if (movies != null)
                return Ok(movies);

            return BadRequest();
        }

        // GET: api/<MovieController>/TopRated
        [HttpGet("[action]")]
        public async Task<IActionResult> TopRated()
        {
            var movies = await _movieRepository.GetTopRatedMovies();

            if (movies != null)
                return Ok(movies);

            return BadRequest();
        }

        // GET: api/<MovieController>/Upcoming
        [HttpGet("[action]")]
        public async Task<IActionResult> Upcoming()
        {
            var movies = await _movieRepository.GetUpcomingMovies();

            if (movies != null)
                return Ok(movies);

            return BadRequest();
        }

        // GET api/<MovieController>/query
        [HttpGet("{query}")]
        public async Task<IActionResult> Get(string query)
        {
            var movies = await _movieRepository.GetQueriedMovies(query);

            if (movies != null)
                return Ok(movies);

            return BadRequest();
        }
    }
}