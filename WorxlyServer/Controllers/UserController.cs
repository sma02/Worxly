using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorxlyServer.Data;
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
            user.PasswordHash = user.Password;
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
    }
}
