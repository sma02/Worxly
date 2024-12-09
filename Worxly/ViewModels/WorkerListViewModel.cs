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
using System.Text;
using System.Threading.Tasks;
using Worxly.Api;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class WorkerListViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<Worker> Workers => workers;
        public ReactiveCommand<Worker, Unit> WorkerButtonCommand { get; set; }
        private SourceCache<Worker, int> sourceCache = new(x => x.Id);
        private string searchText = "";
        private ReadOnlyObservableCollection<Worker> workers;

        private Service service;

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                sourceCache.Refresh();
            }
        }
        public WorkerListViewModel(Service s)
        {
            int serviceId = s?.Id ?? throw new Exception("Service is null");
            service = s;
            Init(serviceId);
            WorkerButtonCommand = ReactiveCommand.Create<Worker>(WorkerButtonClick);
        }
        public async Task Init(int serviceId)
        {
            var workerApi = RestService.For<IWorkerApi>(Properties.Resources.DefaultHost);
            var workersList = await workerApi.GetWorkersByService(serviceId);
            if (workersList.StatusCode != System.Net.HttpStatusCode.OK)
                return;
            var content = workersList.Content;
            sourceCache.AddOrUpdate(content);
            sourceCache.Connect()
                .Filter(x => x.FirstName.ToLower().Contains(searchText.ToLower())).Bind(out workers)
                .Sort(SortExpressionComparer<Worker>.Ascending(t => t.FirstName))
                .Subscribe();
            this.RaisePropertyChanged(nameof(Workers));
        }
        public async void WorkerButtonClick(Worker worker)
        {
            ConfirmationDialog dialog = new ConfirmationDialog()
            {
                Message = $"Are you sure you want to subscribe {worker.FirstName} for 500rs ?",
                PositiveText = "Yes",
                NegativeText = "No",
                NegativeButtonVisibility = true
            };
            var res = (bool)await dialog.Show();
            if (res)
            {
                var workerApi = RestService.For<IWorkerApi>(Properties.Resources.DefaultHost);
                var work = new Work
                {
                    Provider = worker,
                    Service = service,
                    WorkStatuses = "Pending",
                    CreatedOn = DateTime.Now
                };
                var workPosted = await workerApi.AddWorkInUser(work, Globals.Instance.CurrentUser.Username);
            }
        }
    }
}
