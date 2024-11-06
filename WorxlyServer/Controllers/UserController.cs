using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorxlyServer.Data;
using WorxlyServer.DTOs;
using WorxlyServer.Models;

namespace WorxlyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> PostUserAccount([FromBody] User user)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            User u = new User
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserAccounts()
        {
            var userAccounts = await _context.Users.ToListAsync();
            return Ok(userAccounts);
        }
        [HttpGet("Auth")]
        public async Task<IActionResult> Authenticate(string? identifier, string password)
        {
            if (identifier == null)
                return BadRequest();
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
            if (user == null)
                return NotFound();
            if (user.PasswordHash != HashPassword(password))
                return Unauthorized();
            UserDTO userDTO = new UserDTO(user);
            return Ok(userDTO);
        }
        private string HashPassword(string password)
        {
            return password;
        }
    }
}
