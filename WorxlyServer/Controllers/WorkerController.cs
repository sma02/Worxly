using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorxlyServer.Data;
using WorxlyServer.DTOs;
using WorxlyServer.Models;

namespace WorxlyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly AppDbContext _context;

        public WorkerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetWorkersByService")]
        public async Task<IActionResult> GetWorkersByService(int serviceId)
        {
            var workerAccounts = await _context.Workers.Where(x => x.Services != null)
                .Where(y => y.Services.Any(z => z.Id == serviceId))
                .Select(w => new WorkerDTO(w)).ToListAsync() ?? new List<WorkerDTO>();
            return Ok(workerAccounts);
        }

        [HttpPost("AddWorkInUser")]
        public async Task<IActionResult> AddWorkInUser([FromBody] WorkDTO workDto, string identifier)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == identifier || x.Email == identifier);
            if (user == null)
                return NotFound();

            var work = new Work()
            {
                Id = workDto.Id,
                Provider = new Worker
                {
                    FirstName = workDto.Provider.FirstName,
                    LastName = workDto.Provider.LastName,
                    Bio = workDto.Provider.Bio,
                },
                Service = new Service
                {
                    Name = workDto.Service.Name,
                    Description = workDto.Service.Description
                },
                WorkStatuses = new List<WorkStatus>()
                {
                    new WorkStatus(){ WorkStatusType = (WorkStatusType) Enum.Parse(typeof(WorkStatusType), workDto.WorkStatuses, true)}
                },
                CreatedOn = workDto.CreatedOn
            };

            _context.Works.Add(work);
            user.WorkSubscriptions.Add(work);

            await _context.SaveChangesAsync();
            return Ok(work);
        }
    }
}
