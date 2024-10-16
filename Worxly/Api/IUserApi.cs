using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worxly.Api
{
    public interface IUserApi
    {
        [Get("/api/user")]
        Task<List<User>> GetUserAccounts();
    }
}
