using Foolproof;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorxlyServer.Models
{
    [Table("WorkRecords")]
    public class Work
    {
        public int Id { get; set; }
        public Worker Provider { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public IEnumerable<WorkStatus>? WorkStatuses { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? WhenDeleted { get; set; }
    }
}
