using WorxlyServer.Models;
namespace WorxlyServer.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private string? imageUrl;
        public ServiceDTO(Service service)
        {
            Name = service.Name;
            Description = service.Description;
            imageUrl = service.Image;
        }
        public ServiceDTO()
        { 
        }
    }
}
