using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs;

namespace Worxly.Api
{
    public interface IServiceApi
    {
        [Post("/api/Services")]
        Task<ApiResponse<Service>> PostService(Service serviceDto);
      
        [Get("/api/Services")]
        Task<ApiResponse<List<Service>>> GetServices();

        [Put("/api/Services/{id}")]
        Task<ApiResponse<Service>> PutService(int id,Service serviceDto);

        [Delete("/api/Services/{id}")]
        Task<ApiResponse<Service>> DeleteService(int id);
    }
}
