using System.Security.Cryptography;
using System.Text;
using doughdash_api.Data;
using doughdash_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace doughdash_api.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly DoughDashContext _context;
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger, DoughDashContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <summary>
    ///     Generate the API Access Code.
    /// </summary>
    /// <param name="password">Server Global Password</param>
    /// <returns>Api Access Code</returns>
    [HttpPost]
    [Route("getAccessToken")]
    public IActionResult GenerateAccessCode(string? password)
    {
        // Validate input
        if (password == null) return BadRequest("Password is required");

        // Verify password
        var hashedPassword = CalculateSha256(password);

        _logger.LogInformation($"Hashed password: {hashedPassword}");

        if (hashedPassword != CalculateSha256("TEST")) return BadRequest("Invalid password");

        // Generate a random 8-character access code
        var accessCode = GenerateRandomAccessCode();

        try
        {
            // Ensure the generated code is unique
            while (_context.AccessCodes.Any(e => e.Code == accessCode)) accessCode = GenerateRandomAccessCode();

            // Create a new AccessCode entity
            var newAccessCode = new AccessCode
            {
                Code = accessCode
            };

            // Add the new entity to the database
            _context.AccessCodes.Add(newAccessCode);

            // Save changes to the database
            _context.SaveChanges();

            // Return the access code
            return Ok(new { AccessCode = accessCode });
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "Error generating access code");
            return StatusCode(500, "An error occurred while generating the access code");
        }
    }

    // Helper method to generate a random 8-character access code
    private string GenerateRandomAccessCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private string CalculateSha256(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Convert the password string to a byte array
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Compute the hash
            var hashBytes = sha256.ComputeHash(passwordBytes);

            // Convert the byte array to a string of hexadecimal values
            var builder = new StringBuilder();
            for (var i = 0; i < hashBytes.Length; i++) builder.Append(hashBytes[i].ToString("x2"));

            return builder.ToString();
        }
    }
}