using WorxlyServer.Models;

namespace WorxlyServer.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageFile { get; set; }
        public ServiceDTO() { }

        public ServiceDTO(Service service)
        {
            Id = service.Id;
            Name = service.Name;
            Description = service.Description!;
            ImageFile = service.Image;
        }
    }
}
