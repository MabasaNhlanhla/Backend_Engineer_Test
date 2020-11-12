using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IChuckService
    {
        public Task<HttpResponseMessage> GetJokeCategories();

        public Task<HttpResponseMessage> SearchJokes(string query);
    }
}
