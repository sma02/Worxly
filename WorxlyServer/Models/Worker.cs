using System.ComponentModel.DataAnnotations.Schema;

namespace WorxlyServer.Models
{
    public enum WorkerType
    { Electrician, Plumber, Carpenter, Painter, Gardener, Cleaner, Other }
    [Table("Workers")]
    public class Worker : User
    {
        public string? Bio { get; set; }
        public WorkerType Type { get; set; }
        public IEnumerable<Service>? Services { get; set; }
        public IEnumerable<Rating>? Ratings { get; set; }
        public float? OverallRating { get; set; }
        public Worker() { }
    }
}
