using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    public class ChatMessage
    {
        [JsonIgnore]
        public string Id { get; set; }
        public required int ChatId { get; set; }
        public Chat Chat { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public required string Message { get; set; }
        [JsonIgnore]
        public DateTime? WhenDeleted { get; set; }

    }
}
