using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public class ChuckService : IChuckService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChuckService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetJokeCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.chucknorris.io/jokes/categories/");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> SearchJokes(string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.chucknorris.io/jokes/search?query={query}");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response;
        }
    }
}
