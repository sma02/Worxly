using System.ComponentModel.DataAnnotations.Schema;

namespace WorxlyServer.Models
{
    [Table("Workers")]
    public class Worker : User
    {
        public string? Bio { get; set; }
        public WorkerCategory? Category { get; set; }
        public IEnumerable<Service>? Services { get; set; }
        public IEnumerable<Rating>? Ratings { get; set; }
        public float? OverallRating { get; set; }
        public Worker() 
        {
            UserType = UserType.Worker;
        }
    }
}
