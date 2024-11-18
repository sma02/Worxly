namespace WorxlyServer.Models
{
    public class WorkStatus
    {
        public int Id { get; set; }
        public required WorkStatusType WorkStatusType { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}
