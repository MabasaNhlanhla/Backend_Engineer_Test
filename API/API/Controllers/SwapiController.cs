using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        private readonly ISwapiService _swapiService;
        public SwapiController(ISwapiService swapiService)
        {
            _swapiService = swapiService;
        }

        [HttpGet("~/people")]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _swapiService.GetPeople();
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            else
            {
                return new ObjectResult(response.StatusCode);
            }
        }
    }
}