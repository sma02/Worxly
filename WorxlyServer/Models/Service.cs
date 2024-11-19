using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    [Table("Services")]
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? WhenDeleted { get; set; }
    }
}
