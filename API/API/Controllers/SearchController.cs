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
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IChuckService _chuckService;
        private readonly ISwapiService _swapiService;
        private readonly ILogger<SearchController> _logger;
        public SearchController(IChuckService chuckService, ISwapiService swapiService, ILogger<SearchController> logger)
        {
            _chuckService = chuckService;
            _swapiService = swapiService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery]string query = "")
        {
            _logger.LogInformation(1, "Fetching search query");

            if (query != "")
            {
                HttpResponseMessage jokes = null, people = null;
                Parallel.Invoke(() => jokes = _chuckService.SearchJokes(query).Result,
                    () => people = _swapiService.SearchPeople(query).Result);

                ICollection<string> results = await SearchResults(jokes, people);

                if (results.Count != 0)
                {
                    _logger.LogInformation(2, "Search query retrieved successfully");

                    return Ok(results);
                }
                else
                {
                    _logger.LogInformation(3, "Something went wrong while fetching categories");

                    return NotFound();
                }
            }
            else
            {
                _logger.LogInformation(4, "The given search query was malformed");

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