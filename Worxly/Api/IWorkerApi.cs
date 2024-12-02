using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs;

namespace Worxly.Api
{
    public interface IWorkerApi
    {
        [Get("/api/worker/GetWorkersByService")]
        Task<ApiResponse<List<Worker>>> GetWorkersByService(int serviceId);

        [Post("/api/worker/AddWorkInUser")]
        Task<ApiResponse<Work>> AddWorkInUser(Work workDto, string identifier);
    }
}
