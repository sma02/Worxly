using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    public class Chat
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int WorkerId { get; set; }
        [JsonIgnore]
        public DateTime? WhenDeleted { get; set; }
    }
}
