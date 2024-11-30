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
        Task<Service> PostService(Service serviceDto);
      
        [Get("/api/Services")]
        Task<List<Service>> GetServices();

        [Put("/api/Services/{id}")]
        Task<Service> PutService(int id,Service serviceDto);

        [Delete("/api/Services/{id}")]
        Task<Service> DeleteService(int id);

    }
}
