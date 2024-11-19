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
    }
}
