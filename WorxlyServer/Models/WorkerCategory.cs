using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    public class WorkerCategory
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Image { get; set; }
        public IEnumerable<Service>? Services { get; set; }
        [JsonIgnore]
        public DateTime? WhenDeleted { get; set; }
    }
}
