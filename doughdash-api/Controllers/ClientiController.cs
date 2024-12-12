using doughdash_api.Data;
using doughdash_api.Models;
using doughdash_api.Utility;
using Microsoft.AspNetCore.Mvc;

namespace doughdash_api.Controllers;

[ApiController]
public class ClientiController : ControllerBase
{
    private readonly DoughDashContext _context;
    private readonly ILogger<ClientiController> _logger;

    public ClientiController(DoughDashContext context, ILogger<ClientiController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    [Route("/getCliente")]
    public IActionResult GetById(int? id, string? accessToken)
    {
        if (id == null || accessToken == null) return BadRequest("Id is required");

        var authorization = new Authorization(_context);
        if (!authorization.CheckAccessCode(accessToken)) return Unauthorized("Invalid access token");

        var cliente = _context.Clienti.Find(id);

        if (cliente == null) return NotFound("None");

        return Ok(cliente);
    }

    [HttpPost]
    [Route("/getAllClienti")]
    public IActionResult GetAllClienti(string? accessToken)
    {
        if (accessToken == null) return BadRequest("Access token is required");

        var authorization = new Authorization(_context);
        if (!authorization.CheckAccessCode(accessToken)) return Unauthorized("Invalid access token");

        return Ok(_context.Clienti.ToList());
    }

    [HttpPost]
    [Route("/addCliente")]
    public IActionResult AddCliente([FromBody] Cliente newCliente, [FromQuery] string? accessToken)
    {
        if (string.IsNullOrEmpty(accessToken)) return BadRequest("Cliente data and access token are required");

        var authorization = new Authorization(_context);
        if (!authorization.CheckAccessCode(accessToken)) return Unauthorized("Invalid access token");

        newCliente.Id = 0;

        try
        {
            _context.Clienti.Add(newCliente);
            _context.SaveChanges();
            return Created($"/getCliente?id={newCliente.Id}", newCliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving the Cliente to the database.");
            return StatusCode(500, "Internal server error occurred while saving the Cliente.");
        }
    }
}