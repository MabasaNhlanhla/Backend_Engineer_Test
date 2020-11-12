using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IChuckService _chuckService;
        private readonly ISwapiService _swapiService;
        public SearchController(IChuckService chuckService, ISwapiService swapiService)
        {
            _chuckService = chuckService;
            _swapiService = swapiService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery]string query = "")
        {
            if(query != "")
            {
                HttpResponseMessage jokes = null, people = null;
                Parallel.Invoke(() => jokes = _chuckService.SearchJokes(query).Result,
                    () => people = _swapiService.SearchPeople(query).Result);

                ICollection<string> results = await SearchResults(jokes, people);

                if (results.Count != 0)
                {
                    return Ok(results);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        private async Task<ICollection<string>> SearchResults(HttpResponseMessage jokes, HttpResponseMessage people)
        {
            ICollection<string> results = new Collection<string>();
            if (jokes.IsSuccessStatusCode)
            {
                var result = await jokes.Content.ReadAsStringAsync();
                results.Add(result);
            }
            if (people.IsSuccessStatusCode)
            {
                var result = await people.Content.ReadAsStringAsync();
                results.Add(result);
            }
            return results;
        }
    }
}