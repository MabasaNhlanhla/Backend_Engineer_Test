using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuckController : ControllerBase
    {
        private readonly IChuckService _chuckService;
        private readonly ILogger<ChuckController> _logger;

        public ChuckController(IChuckService chuckService, ILogger<ChuckController> logger)
        {
            _chuckService = chuckService;
            _logger = logger;

        }

        [HttpGet("~/categories")]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation(1, "Fetching categories");

            var responseMessage = await _chuckService.GetJokeCategories();
            if (responseMessage.IsSuccessStatusCode)
            {
                _logger.LogInformation(2, "Categories fetched successfully");

                var categories = await responseMessage.Content.ReadAsStringAsync();
                return Ok(categories);
            }
            else
            {
                _logger.LogInformation(3, "Something went wrong while fetching categories");

                return new ObjectResult(responseMessage.StatusCode);
            }
        }
    }
}