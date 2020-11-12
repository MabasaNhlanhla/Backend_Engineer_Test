using API.Interfaces;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public class SwapiService : ISwapiService
    {
        IHttpClientFactory _httpClientFactory;
        public SwapiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetPeople()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://swapi.dev/api/people/");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> SearchPeople(string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://swapi.dev/api/people/?search={query}");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response;

        }
    }
}
