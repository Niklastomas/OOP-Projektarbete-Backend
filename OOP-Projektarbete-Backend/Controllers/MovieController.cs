using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OOP_Projektarbete_Backend.Caching;
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
        private IMemoryCache _cache;

        public MovieController(IMovieRepository movieRepository,
            IMemoryCache cache)
        {
            _movieRepository = movieRepository;
            _cache = cache;
        }

        // GET: api/<MovieController>/Trending
        [HttpGet("[action]")]
        public async Task<IActionResult> Trending()
        {
            var movies = await _cache.GetOrCreateAsync(CacheKeys.TrendingMovies, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return await _movieRepository.GetTrendingMovies();
            });

            if (movies != null )
                return Ok(movies);
  
            return NotFound();
       
        }

        // GET: api/<MovieController>/Popular
        [HttpGet("[action]/{page}")]
        public async Task<IActionResult> Popular(string page)
        {
            var movies = await _cache.GetOrCreateAsync(CacheKeys.PopularMovies, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return await _movieRepository.GetPopularMovies(page);
            });

            if (movies != null)
                return Ok(movies);

            return NotFound();
        }

        // GET: api/<MovieController>/TopRated
        [HttpGet("[action]/{page}")]
        public async Task<IActionResult> TopRated(string page)
        {
            var movies = await _cache.GetOrCreateAsync(CacheKeys.TopRatedMovies, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return await _movieRepository.GetTopRatedMovies(page);
            });

            if (movies != null)
                return Ok(movies);

            return NotFound();
        }

        // GET: api/<MovieController>/Upcoming
        [HttpGet("[action]/{page}")]
        public async Task<IActionResult> Upcoming(string page)
        {
            var movies = await _cache.GetOrCreateAsync(CacheKeys.UpcomingMovies, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return await _movieRepository.GetUpcomingMovies(page);
            });

            if (movies != null)
                return Ok(movies);

            return NotFound();
        }

        // GET api/<MovieController>/query/page
        [HttpGet("{query}/{page}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(string query, string page)
        {
            var movies = await _movieRepository.GetQueriedMovies(query, page);

            if (movies != null)
                return Ok(movies);

            return NotFound();
        }

        // GET api/MovieDetails/id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> MovieDetails(string id)
        {
            var movie = await _cache.GetOrCreateAsync(id, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                var movie = await _movieRepository.GetMovieDetails(id);
                var trailer = await _movieRepository.GetMovieTrailer(id);
                return new MovieDTO()
                {
                    Movie = movie,
                    Trailer = trailer
                };

            });
            if (movie != null)
                return Ok(movie);

            return NotFound();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Trailer(string id)
        {
            var trailer = await _movieRepository.GetMovieTrailer(id);

            if (trailer != null)
                return Ok(trailer);

            return NotFound();
        }
    }
}