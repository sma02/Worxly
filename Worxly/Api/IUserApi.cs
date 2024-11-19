using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs;

namespace Worxly.Api
{
    public interface IUserApi
    {
        //[Get("/api/user")]
        //Task<List<User>> GetUserAccounts();

        [Post("/api/user")]
        Task<ApiResponse<UserCreate>> PostUserAccount(UserCreate userDto);

        [Get("/api/user/Auth")]
        Task<ApiResponse<UserStore>> Authenticate(string? identifier, string password);

        [Get("/api/user/GetUserDetails")]
        Task<ApiResponse<UserCreate>> GetUserDetails(string? identifier, string password);

    }
}
