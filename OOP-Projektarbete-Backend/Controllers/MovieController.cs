using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OOP_Projektarbete_Backend.DTOs;
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
    [EnableCors("CORSPolicy")]
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
        [HttpGet("[action]/{page}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Popular(string page)
        {
            var movies = await _movieRepository.GetPopularMovies(page);

            if (movies != null)
                return Ok(movies);

            return BadRequest();
        }

        // GET: api/<MovieController>/TopRated
        [HttpGet("[action]/{page}")]
        public async Task<IActionResult> TopRated(string page)
        {
            var movies = await _movieRepository.GetTopRatedMovies(page);

            if (movies != null)
                return Ok(movies);

            return BadRequest();
        }

        // GET: api/<MovieController>/Upcoming
        [HttpGet("[action]/{page}")]
        public async Task<IActionResult> Upcoming(string page)
        {
            var movies = await _movieRepository.GetUpcomingMovies(page);

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

        // GET api/<MovieController>/query
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> MovieDetails(string id)
        {
            var movie = await _movieRepository.GetMovieDetails(id);
            var trailer = await _movieRepository.GetMovieTrailer(id);

            if (movie != null)
            {
                var movieDTO = new MovieDTO()
                {
                    Movie = movie,
                    Trailer = trailer
                };

                return Ok(movieDTO);
            }

            return BadRequest();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Trailer(string id)
        {
            var trailer = await _movieRepository.GetMovieTrailer(id);

            if (trailer != null)
                return Ok(trailer);

            return BadRequest();
        }
    }
}