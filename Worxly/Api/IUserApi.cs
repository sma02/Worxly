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
        Task<ApiResponse<User>> PostUserAccount(User userDto);

        [Get("/api/user/Auth")]
        Task<ApiResponse<User>> Authenticate(string? identifier, string password);

        [Get("/api/user/GetUserDetails")]
        Task<ApiResponse<User>> GetUserDetails(string? identifier, string password);

        [Get("/api/user/GetUserWorks")]
        Task<ApiResponse<List<Work>>> GetUserWorks(string identifier);
    }
}
