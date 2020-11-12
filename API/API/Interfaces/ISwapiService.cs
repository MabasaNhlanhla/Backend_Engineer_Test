using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ISwapiService
    {
        public Task<HttpResponseMessage> GetPeople();

        public Task<HttpResponseMessage> SearchPeople(string query);
    }
}
