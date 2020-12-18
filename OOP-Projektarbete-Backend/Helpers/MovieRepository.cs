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
        private IHttpClientFactory _clientFactory;
        private IConfiguration _configuration;

        public MovieRepository(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<MovieInfo> GetTrendingMovies()
        {
            var client = _clientFactory.CreateClient("movie");
            var apiKey = _configuration.GetValue<string>("APIKey");

            try
            {
                return await client.GetFromJsonAsync<MovieInfo>($"trending/all/day?api_key={apiKey}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<MovieInfo> GetPopularMovies()
        {
            var client = _clientFactory.CreateClient("movie");
            var apiKey = _configuration.GetValue<string>("APIKey");

            try
            {
                return await client.GetFromJsonAsync<MovieInfo>($"movie/popular?api_key={apiKey}&language=en-US&page=1");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<MovieInfo> GetTopRatedMovies()
        {
            var client = _clientFactory.CreateClient("movie");
            var apiKey = _configuration.GetValue<string>("APIKey");

            try
            {
                return await client.GetFromJsonAsync<MovieInfo>($"movie/top_rated?api_key={apiKey}&language=en-US&page=1");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<MovieInfo> GetUpcomingMovies()
        {
            var client = _clientFactory.CreateClient("movie");
            var apiKey = _configuration.GetValue<string>("APIKey");

            try
            {
                return await client.GetFromJsonAsync<MovieInfo>($"movie/upcoming?api_key={apiKey}&language=en-US&page=1");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<MovieInfo> GetQueriedMovies(string query)
        {
            var client = _clientFactory.CreateClient("movie");
            var apiKey = _configuration.GetValue<string>("APIKey");

            try
            {
                return await client.GetFromJsonAsync<MovieInfo>($"search/movie?api_key={apiKey}&language=en-US&page=1&include_adult=false&{query}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}