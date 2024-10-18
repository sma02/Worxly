using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    [Table("Ratings")]
    public class Rating
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required User User { get; set; }
        public required int RatingValue { get; set; }
        public string? Comment { get; set; }
        [JsonIgnore]
        public DateTime? WhenDeleted { get; set; }
    }
}
