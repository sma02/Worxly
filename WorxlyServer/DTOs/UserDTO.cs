using Microsoft.OpenApi.Extensions;
using WorxlyServer.Models;

namespace WorxlyServer.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserTypeVal { get; set; }
        public UserDTO(User user)
        {
            Username = user.Username;
            Email = user.Email;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserTypeVal = user.UserType.GetDisplayName();
        }
        public UserDTO()
        {

        }
    }
}
