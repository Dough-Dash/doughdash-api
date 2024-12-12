using doughdash_api.Data;
using doughdash_api.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doughdash_api.Controllers;

[ApiController]
public class RidersController : ControllerBase
{
    private readonly DoughDashContext _context;

    public RidersController(DoughDashContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Get a single rider by id
    /// </summary>
    [HttpPost]
    [Route("/getRider")]
    [Produces("application/json")]
    public IActionResult GetById(int? id, string accessToken)
    {
        var authorization = new Authorization(_context);
        if (!authorization.CheckAccessCode(accessToken)) return Unauthorized("Invalid access token");

        if (id == null) return BadRequest("Id is required");

        var rider = _context.Riders.Find(id);

        if (rider == null) return NotFound("None");

        return Ok(rider);
    }

    /// <summary>
    ///     Get all riders
    /// </summary>
    [HttpPost]
    [Route("/getAllRiders")]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllRiders(string? accessToken)
    {
        if (accessToken == null) return BadRequest("Access token is required");

        var authorization = new Authorization(_context);
        if (!authorization.CheckAccessCode(accessToken)) return Unauthorized("Invalid access token");

        return Ok(await _context.Riders.ToListAsync());
    }
}