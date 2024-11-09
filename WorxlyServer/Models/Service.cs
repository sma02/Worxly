using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    [Table("Services")]
    public class Service
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [JsonIgnore]
        public DateTime? WhenDeleted { get; set; }
    }
}
