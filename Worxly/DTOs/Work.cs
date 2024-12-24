using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs.Enums;
using Worxly.DTOs.Interfaces;

namespace Worxly.DTOs
{
    public class Work
    {
        public int Id { get; set; }
        public Worker Provider { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public string? WorkStatus { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
