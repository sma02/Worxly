using Microsoft.OpenApi.Extensions;
using WorxlyServer.Models;

namespace WorxlyServer.DTOs
{
    public class WorkerDTO : UserDTO
    {
        public string? Bio { get; set; }
        public float? OverallRating { get; set; }
        public WorkerDTO()
        {

        }
        public WorkerDTO(Worker worker) 
        {
            Username = worker.Username;
            Email = worker.Email;
            FirstName = worker.FirstName;
            LastName = worker.LastName;
            UserTypeVal = worker.UserType.GetDisplayName();
            Bio = worker.Bio;
            OverallRating = worker.OverallRating;
        }
    }
}
