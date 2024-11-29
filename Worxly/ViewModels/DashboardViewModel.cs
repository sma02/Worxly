using DynamicData.Binding;
using DynamicData;
using ReactiveUI;
using System.Reactive;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.Api;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private User user;
        public User User
        {
            get => user;
            set => this.RaiseAndSetIfChanged(ref user, value);
        }
        public ReadOnlyObservableCollection<Work> Subscriptions => subscriptions;
        private SourceCache<Work, int> sourceCache = new(x => x.Id);
        private string searchText = "";
        private ReadOnlyObservableCollection<Work> subscriptions;

        public ReactiveCommand<Work, Unit> SubscriptionButtonCommand { get; set; }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                sourceCache.Refresh();
            }
        }

        public DashboardViewModel(User user)
        {
            User = user;
            LoadSubscriptions(user);
            SubscriptionButtonCommand = ReactiveCommand.Create<Work>(SubscriptionButtonClick);
        }

        public void SubscriptionButtonClick(Work work)
        {
            // throw new NotImplementedException();
        }
        public async void LoadSubscriptions(User user)
        {
            //var subscriptionsList = WorksList();

            var userApi = RestService.For<IUserApi>(Properties.Resources.DefaultHost);
            var res = await userApi.GetUserWorks(user.Username);

            var subscriptionsList = res.Select(dto => new Work
            {
                Provider = new Worker
                {
                    FirstName = dto.Provider.FirstName,
                    LastName = dto.Provider.LastName,
                    Bio = dto.Provider.Bio,
                },
                Service = new Service
                {
                    Name = dto.Service.Name,
                    Description = dto.Service.Description
                },
                WorkStatuses = dto.WorkStatuses,
                CreatedOn = dto.CreatedOn
            }).ToList();

            sourceCache.AddOrUpdate(subscriptionsList);
            sourceCache.Connect()
                .Filter(x => x.Service.Name.ToLower().Contains(searchText.ToLower())).Bind(out subscriptions)
                .Sort(SortExpressionComparer<Work>.Ascending(t => t.Service.Name))
                .Subscribe();
        }

        public List<Work> WorksList()
        {
            List<Work> works = new List<Work>
            {
                new Work
                {
                    Provider = new Worker
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Bio = "Expert in plumbing",
                    },
                    Service = new Service
                    {
                        Name = "Plumbing",
                        Description = "Fixing leaks and installations",
                    },
                    WorkStatuses = "Completed",
                    CreatedOn = DateTime.Now.AddDays(-3)
                },
                new Work
                {
                    Provider = new Worker
                    {
                        FirstName = "Jane",
                        LastName = "Smith",
                        Bio = "Skilled carpenter",
                    },
                    Service = new Service
                    {
                        Name = "Carpentry",
                        Description = "Building and fixing wooden structures",
                    },
                    WorkStatuses = "In Progress",
                    CreatedOn = DateTime.Now.AddDays(-1)
                }
            };
            return works;
        }
    }
}
