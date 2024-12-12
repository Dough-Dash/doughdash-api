using doughdash_api.Data;
using doughdash_api.Models;
using doughdash_api.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doughdash_api.Controllers;

[ApiController]
public class OrdersController : ControllerBase
{
    private readonly DoughDashContext _context;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger, DoughDashContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    [Route("/getOrder")]
    public IActionResult GetOrderById(int? id, string? accessCode)
    {
        if (id == null || accessCode == null) return BadRequest("Id is required");

        var authorization = new Authorization(_context);
        if (!authorization.CheckAccessCode(accessCode)) return Unauthorized("Invalid access token");

        var ordine = _context.Ordini.FirstOrDefault(e => e.IDOrdine == id);

        if (ordine == null) return NotFound();

        return Ok(ordine);
    }

    [HttpPost]
    [Route("/getAllOrders")]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllRiders(string? accessToken)
    {
        if (accessToken == null) return BadRequest("Access token is required");

        var authorization = new Authorization(_context);
        if (!authorization.CheckAccessCode(accessToken)) return Unauthorized("Invalid access token");

        return Ok(await _context.Riders.ToListAsync());
    }

    [HttpPost]
    [Route("/addOrder")]
    [Produces("application/json")]
    public async Task<IActionResult> AddOrder([FromBody] Ordine order, string? accessCode)
    {
        if (accessCode == null) return BadRequest("Access token is required");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Explicitly set ID to 0 to allow auto-increment
        order.IDOrdine = 0;

        // Set the current timestamp if not already set
        order.Orario = DateTime.UtcNow;

        try
        {
            _context.Ordini.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding order");
            return StatusCode(500, ex.Message);
        }
    }
}