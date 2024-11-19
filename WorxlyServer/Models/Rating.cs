using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    [Table("Ratings")]
    public class Rating
    {
        public int Id { get; set; }
        public User User { get; set; } = null!;
        public required int RatingValue { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? WhenDeleted { get; set; }
    }
}
