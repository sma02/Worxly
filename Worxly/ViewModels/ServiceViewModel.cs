using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        public ObservableCollection<Service> Services { get; set; }
        public ReactiveCommand<Unit, Unit> AddButtonCommand { get; set; }
        public ServiceViewModel()
        {
            Services = [];
            AddButtonCommand = ReactiveCommand.Create(AddButtonClick);
        }
        public void AddButtonClick()
        {
            throw new NotImplementedException();
        }
    }
}
