using doughdash_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace doughdash_api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PizzerieController : ControllerBase
    {
        private readonly DoughDashContext _context;

        public PizzerieController(DoughDashContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("/getPizzeria")]
        public IActionResult GetById(int? id, string accessToken)
        {
            if (accessToken != "TEST")
            {
                return Unauthorized("Invalid access token");
            }

            if (id == null)
            {
                return BadRequest("Id is required");
            }

            var rider = _context.Pizzerie.Find(id);

            if (rider == null)
            {
                return NotFound("None");
            }

            return Ok(rider);
        }
    }
}
