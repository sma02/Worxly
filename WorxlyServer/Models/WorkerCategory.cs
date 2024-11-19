using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    [Table("WorkerCategories")]
    public class WorkerCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Image { get; set; }
        public IEnumerable<Service>? Services { get; set; }
        public DateTime? WhenDeleted { get; set; }
    }
}
