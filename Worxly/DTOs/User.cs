using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs.Interfaces;

namespace Worxly.DTOs
{
    public class User : IUserAuth, IUserIdentity
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string UserTypeVal { get; set; }
    }
}
