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
        [JsonIgnore]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }
        [JsonIgnore]
        public Address? Address { get; set; }
        public string? Phone { get; set; }
        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public required string Password { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
        [JsonIgnore]
        public DateTime? WhenDeleted { get; set; }
        [JsonIgnore]
        public UserType UserType { get; set; } = UserType.User;
    }
}
