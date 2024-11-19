using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly PasswordHasher<User> _passwordHasher;
        public UserController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }
        [HttpPost]
        public async Task<IActionResult> PostUserAccount([FromBody] UserDTO userDto)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Email = userDto.Email,
            };
            user.UserType = userDto.UserTypeVal switch
            {
                "Admin" => UserType.Admin,
                "User" => UserType.User,
                "Worker" => UserType.Worker
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);
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
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verificationResult == PasswordVerificationResult.Failed)
                return Unauthorized();
            UserDTO userDTO = new UserDTO(user);
            return Ok(userDTO);
        }

        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(string? identifier, string password)
        {
            if (identifier == null)
                return BadRequest();
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
            if (user == null)
                return NotFound();
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verificationResult == PasswordVerificationResult.Failed)
                return Unauthorized();
            return Ok(user);
        }
    }
}
