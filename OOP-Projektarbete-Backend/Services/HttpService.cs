using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Helpers
{
    public class HttpService : IHttpService
    {
        private IHttpClientFactory _clientFactory;

        public HttpService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> Get<T>(string url)
        {
            var client = _clientFactory.CreateClient("movie");

            try
            {
                return await client.GetFromJsonAsync<T>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
    }
}