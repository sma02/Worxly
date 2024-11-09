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
        [Get("/api/service")]
        Task<List<Service>> GetUserServices(User user);

        [Post("/api/service")]
        Task<Service> PostService(Service serviceDto);
    }
}
