using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.Api;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<Service> Services => services;
        public ReactiveCommand<Service, Unit> ServiceButtonCommand { get; set; }
        private SourceCache<Service, int> sourceCache = new(x => x.Id);
        private string searchText = "";
        private ReadOnlyObservableCollection<Service> services;

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                sourceCache.Refresh();
            }
        }

        public UserViewModel()
        {
            var serviceApi = RestService.For<IServiceApi>(Properties.Resources.DefaultHost);
            var servicesList = serviceApi.GetServices().Result;
            sourceCache.AddOrUpdate(servicesList);
            sourceCache.Connect()
                .Filter(x => x.Name.ToLower().Contains(searchText.ToLower())).Bind(out services)
                .Sort(SortExpressionComparer<Service>.Ascending(t => t.Name))
                .Subscribe();
            ServiceButtonCommand = ReactiveCommand.Create<Service>(ServiceButtonClick);
        }
        public void ServiceButtonClick(Service service)
        {
            Debug.WriteLine(service.Name);
        }
    }
}
