using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorxlyServer.Data;
using WorxlyServer.DTOs;

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
    }
}
