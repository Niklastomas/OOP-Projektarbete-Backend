﻿using Microsoft.Extensions.Configuration;
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
    }
}