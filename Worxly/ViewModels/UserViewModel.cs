using Avalonia.Threading;
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
using System.Runtime.CompilerServices;
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
                this.RaiseAndSetIfChanged(ref searchText, value);
                sourceCache.Refresh();
            }
        }

        public UserViewModel()
        {
            Init();
            ServiceButtonCommand = ReactiveCommand.Create<Service>(ServiceButtonClick);
        }
        public async Task Init()
        {
            var serviceApi = RestService.For<IServiceApi>(Properties.Resources.DefaultHost);
            var servicesList = await serviceApi.GetServices();
            if (servicesList.StatusCode != System.Net.HttpStatusCode.OK)
                return;
            var content = servicesList.Content;
            sourceCache.AddOrUpdate(content);
            sourceCache.Connect()
                    .Filter(x => x.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).Bind(out services)
                    .Sort(SortExpressionComparer<Service>.Ascending(t => t.Name))
                    .Subscribe();

            this.RaisePropertyChanged(nameof(Services));
        }
        public void ServiceButtonClick(Service service)
        {
            Globals.Instance.Router.Navigate.Execute(new WorkerListViewModel(service));
        }
    }
}
