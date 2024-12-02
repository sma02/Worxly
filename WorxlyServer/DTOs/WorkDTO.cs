using static WorxlyServer.DTOs.WorkDTO;

namespace WorxlyServer.DTOs
{
    public class WorkDTO
    {
        public int Id { get; set; }
        public WorkerDTO Provider { get; set; }
        public ServiceDTO Service { get; set; }
        public string? WorkStatuses { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
