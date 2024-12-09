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
using System.Diagnostics;

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
            var userApi = RestService.For<IUserApi>(Properties.Resources.DefaultHost);
            var res = await userApi.GetUserWorks(user.Username);
            var content = res.Content;
            var subscriptionsList = content.Select((dto) => new Work
            {
                Id = dto.Id,
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

            sourceCache.Clear(); // Clear existing cache
            sourceCache.AddOrUpdate(subscriptionsList);

            sourceCache.Connect()
                .Filter(x => string.IsNullOrEmpty(searchText) ||
                    x.Service.Name.ToLower().Contains(searchText.ToLower()) ||
                    x.Service.Description.ToLower().Contains(searchText.ToLower()) ||
                    x.Provider.FirstName.ToLower().Contains(searchText.ToLower()) ||
                    x.Provider.LastName.ToLower().Contains(searchText.ToLower())
                )
                .Bind(out subscriptions)
                .Sort(SortExpressionComparer<Work>.Ascending(t => t.Service.Name))
                .Subscribe();

            this.RaisePropertyChanged(nameof(Subscriptions));
        }
    }
}
