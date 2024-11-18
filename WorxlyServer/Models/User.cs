using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public Address? Address { get; set; }
        public IEnumerable<Work>? WorkSubscriptions { get; set; }
        public string? Phone { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? WhenDeleted { get; set; }
        public UserType UserType { get; set; } = UserType.User;
    }
}
