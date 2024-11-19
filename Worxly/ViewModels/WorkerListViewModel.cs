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

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                sourceCache.Refresh();
            }
        }
        public WorkerListViewModel(Service service)
        {
            //var serviceApi = RestService.For<IServiceApi>(Properties.Resources.DefaultHost);
            //var servicesList = serviceApi.GetServices().Result;
            var workersList = new ObservableCollection<Worker>()
            {
                new Worker ()
                {
                    Id = 1,
                    FirstName = "Jon",
                    LastName = "Doe",
                    Phone = "123-456-7890",
                    Bio = "I am a worker",
                    OverallRating = 4.5f
                },
                new Worker ()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Phone = "123-456-7890",
                    Bio = "I am a worker",
                    OverallRating = 4.5f
                },
                new Worker ()
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Smith",
                    Phone = "123-456-7890",
                    Bio = "I am a worker",
                    OverallRating = 4.5f
                },
            };

            sourceCache.AddOrUpdate(workersList);
            sourceCache.Connect()
                .Filter(x => x.FirstName.ToLower().Contains(searchText.ToLower())).Bind(out workers)
                .Sort(SortExpressionComparer<Worker>.Ascending(t => t.FirstName))
                .Subscribe();
            WorkerButtonCommand = ReactiveCommand.Create<Worker>(WorkerButtonClick);
        }
        public void WorkerButtonClick(Worker worker)
        {
            Debug.WriteLine(worker.FirstName);
        }
    }
}
