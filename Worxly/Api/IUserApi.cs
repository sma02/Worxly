using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.Models;

namespace Worxly.Api
{
    public interface IUserApi
    {
        //[Get("/api/user")]
        //Task<List<User>> GetUserAccounts();

        [Post("/api/user")]
        Task<User> PostUserAccount(User userDto);

        //[Post("/api/user/login")]
        //Task<AuthenticationResponse> LoginUser([Body] LoginRequest loginRequest);
    }
}
