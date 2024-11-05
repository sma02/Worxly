using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.Models;

namespace Worxly.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        public ObservableCollection<Service> Services { get; set; }
        public ServiceViewModel()
        {
            Services = [];
        }
    }
}
