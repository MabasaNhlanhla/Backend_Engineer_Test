using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuckController : ControllerBase
    {
        private readonly IChuckService _chuckService;

        public ChuckController(IChuckService chuckService)
        {
            _chuckService = chuckService;
        }

        [HttpGet("~/categories")]
        public async Task<IActionResult> GetAsync()
        {
            var responseMessage = await _chuckService.GetJokeCategories();
            if (responseMessage.IsSuccessStatusCode)
            {
                var categories = await responseMessage.Content.ReadAsStringAsync();
                return Ok(categories);
            }
            else
            {
                return new ObjectResult(responseMessage.StatusCode);
            }
        }
    }
}