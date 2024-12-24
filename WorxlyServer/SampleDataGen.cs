using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorxlyServer.Data;
using WorxlyServer.Models;

namespace WorxlyServer
{
    public static class SampleDataGen
    {
        public static void ClearDatabase(AppDbContext context)
        {
            context.Users.ExecuteDelete();
            context.Categories.ExecuteDelete();
        }
        public static void DatabaseDataGen()
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            AppDbContext context = new AppDbContext(optionsBuilder.Options);
            User user = new User()
            {
                Username = "user1",
                Email = "abc@def.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Anytown",
                    State = "NY",
                    Zip = "12345"
                },
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "pass123");


            WorkerCategory category1 = new WorkerCategory()
            {
                Name = "Electrician",
                Services = new List<Service>()
                {
                    new Service()
                    {
                        Name = "Change Wiring",
                        Description = "I will change your house wiring",
                    },
                     new Service()
                    {
                        Name = "Switch Fixing",
                        Description = "I will fix any malfunctioning switches for you",
                    },
                },
            };

            Service service1 = new Service()
            {
                Name = "Installing Bulb and Fans",
                Description = "I will install bulbs and fans for you",
                Image = "bulb_fan.jpg",
                CreatedOn = DateTime.Now
            };
            Worker worker = new Worker()
            {
                Username = "worker",
                Email = "defff@example.com",
                PasswordHash = "abc567",
                FirstName = "Bob",
                LastName = "Doe",
                Phone = "2234567890",
                Address = new Address()
                {
                    Street = "34355 Main St",
                    City = "Anytown",
                    State = "NY",
                    Zip = "12345"
                },
                Bio = "I am a hard working worker",
                Category = category1,
                Services = new List<Service>()
                {
                    new Service()
                    {
                        Name = "Installing Bulb and Fans",
                        Description = "I will install and bulb or fans for you",
                    }
                },
                Ratings = new List<Rating>()
                {
                    new Rating()
                    {
                        User = user,
                        RatingValue = 4,
                        Comment = "Great worker",
                    },
                }
            };
            worker.PasswordHash = passwordHasher.HashPassword(worker, "passWorker");
            Work work1 = new Work()
            {
                Provider = worker,
                Service = worker.Services.Last(),
                WorkStatuses = new List<WorkStatus>()
                {
                    new WorkStatus()
                    {
                        WorkStatusType = WorkStatusType.Completed,
                        CreatedOn = DateTime.Now,
                    }
                },
                CreatedOn = DateTime.Now,
            };

            user.WorkSubscriptions = new List<Work>()
            {
                work1
            };

            context.Users.Add(user);
            context.Workers.Add(worker);
            context.Works.Add(work1);
            context.SaveChanges();
        }
    }
}
