using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        private readonly ISwapiService _swapiService;
        private readonly ILogger<SwapiController> _logger;
        public SwapiController(ISwapiService swapiService, ILogger<SwapiController> logger)
        {
            _swapiService = swapiService;
            _logger = logger;
        }

        [HttpGet("~/people")]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation(1, "Fetching people");

            var response = await _swapiService.GetPeople();
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(2, "People fetched successfully");

                string result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            else
            {
                _logger.LogInformation(3, "Something went wrong while fetching people");

                return new ObjectResult(response.StatusCode);
            }
        }
    }
}