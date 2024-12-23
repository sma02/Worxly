using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
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
            var workStatus = new WorkStatus() { WorkStatusType = Enum.Parse<WorkStatusType>(workDto.WorkStatus) };
            var worker = await _context.Workers.FirstOrDefaultAsync(x => x.Username == workDto.Provider.Username);
            var service = await _context.Service.FirstOrDefaultAsync(x => x.Id == workDto.Service.Id);
            var work = new Work()
            {
                Provider = worker,
                Service = service,
                WorkStatuses = [workStatus]
            };
            if (user.WorkSubscriptions == null)
                user.WorkSubscriptions = new List<Work>();
            _context.Works.Add(work);
            user.WorkSubscriptions.Add(work);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(work);
        }
    }
}
