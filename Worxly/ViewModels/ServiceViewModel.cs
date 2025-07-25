﻿using DynamicData;
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
using System.Windows.Input;
using Worxly.Api;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<Service> Services => services;
        public ICommand AddButtonCommand { get; set; }
        public ReactiveCommand<Service, Unit> EditServiceCommand { get; set; }
        public ReactiveCommand<Service, Unit> DeleteServiceCommand { get; set; }
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

        public ServiceViewModel()
        {
            Init();
            AddButtonCommand = ReactiveCommand.Create(AddButtonClick);
            EditServiceCommand = ReactiveCommand.Create<Service>(EditServiceClick);
            DeleteServiceCommand = ReactiveCommand.Create<Service>(DeleteServiceClick);
        }
        public async void Init()
        {
            var serviceApi = RestService.For<IServiceApi>(Properties.Resources.DefaultHost);
            var servicesList = await serviceApi.GetServices();
            sourceCache.AddOrUpdate(servicesList.Content);
            sourceCache.Connect()
                .Filter(x => x.Name.ToLower().Contains(searchText.ToLower())).Bind(out services)
                .Sort(SortExpressionComparer<Service>.Ascending(t => t.Name))
                .Subscribe();
            this.RaisePropertyChanged(nameof(Services));
        }
        public void AddButtonClick()
        {
            Globals.Instance.Router.Navigate.Execute(new AddServiceViewModel());
        }

        public void EditServiceClick(Service s)
        {
            Globals.Instance.Router.Navigate.Execute(new AddServiceViewModel(s));
        }
        public void DeleteServiceClick(Service s)
        {
            var serviceApi = RestService.For<IServiceApi>(Properties.Resources.DefaultHost);
            serviceApi.DeleteService(s.Id);
            sourceCache.Remove(s.Id);
        }
    }
}
