using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Worxly.DTOs
{
    public class Worker : User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        //public WorkerCategory? Category { get; set; }
        [JsonPropertyName("overallRating")]
        public float? OverallRating { get; set; }
    }
}
