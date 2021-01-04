using Microsoft.Extensions.Configuration;
using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Helpers
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IHttpService _httpService;
        private readonly string _apiKey;

        public MovieRepository(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _apiKey = configuration.GetValue<string>("APIKey");
        }

        public async Task<MovieInfo> GetTrendingMovies()
        {
            return await _httpService.Get<MovieInfo>($"trending/all/day?api_key={_apiKey}");
        }

        public async Task<MovieInfo> GetPopularMovies(string page)
        {
            return await _httpService.Get<MovieInfo>($"movie/popular?api_key={_apiKey}&language=en-US&page={page}");
        }

        public async Task<MovieInfo> GetTopRatedMovies(string page)
        {
            return await _httpService.Get<MovieInfo>($"movie/top_rated?api_key={_apiKey}&language=en-US&page={page}");
        }

        public async Task<MovieInfo> GetUpcomingMovies(string page)
        {
            return await _httpService.Get<MovieInfo>($"movie/upcoming?api_key={_apiKey}&language=en-US&page={page}");
        }

        public async Task<MovieInfo> GetQueriedMovies(string query, string page)
        {
            return await _httpService.Get<MovieInfo>($"search/movie?api_key={_apiKey}&language=en-US&page=1&include_adult=false&query={query}&page={page}");
        }

        public async Task<Movie> GetMovieDetails(string id)
        {
            return await _httpService.Get<Movie>($"movie/{id}?api_key={_apiKey}&language=en-US");
        }

        public async Task<Trailer> GetMovieTrailer(string id)
        {
            return await _httpService.Get<Trailer>($"movie/{id}/videos?api_key={_apiKey}&language=en-US");
        }
    }
}