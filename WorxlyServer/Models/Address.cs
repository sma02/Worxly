﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorxlyServer.Models
{
    [Table("Addresses")]
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? WhenDeleted { get; set; }
    }
}
