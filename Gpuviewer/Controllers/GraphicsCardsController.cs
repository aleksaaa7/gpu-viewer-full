using Gpuviewer.Models;
using Gpuviewer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gpuviewer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphicsCardsController : ControllerBase
    {
        private readonly IGraphicsCardService _service;

        public GraphicsCardsController(IGraphicsCardService service)
        {
            _service = service;
        }
        
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] CreateGraphicsCard dto)
        {
            var savedCard = await _service.CreateGraphicsCardAsync(dto);
            return Ok(savedCard);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cards = await _service.GetAllCardsAsync();
            return Ok(cards);
        }

        [HttpGet("compare")]
        public async Task<IActionResult> Compare(int id1, int id2)
        {
            var result = await _service.CompareCardsAsync(id1, id2);

            if (result == null)
                return NotFound("Didnt found");

            return Ok(result);
        }
    }
}