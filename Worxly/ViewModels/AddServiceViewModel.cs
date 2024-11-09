using ReactiveUI;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Worxly.Api;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class AddServiceViewModel : ViewModelBase
    {
        private string name = string.Empty;
        private string description = string.Empty;
        private string imageurl = string.Empty;
        public ICommand AddServiceCommand { get; }


        public string Name { 
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value); 
        }
        public string Description { 
            get => description; 
            set => this.RaiseAndSetIfChanged(ref description, value);
        }
        public string Imageurl { 
            get => imageurl; 
            set => this.RaiseAndSetIfChanged(ref imageurl, value);
        }
        public void AddServiceClick()
        {
            var serviceApi= RestService.For<IServiceApi>(Properties.Resources.DefaultHost);
            var service = new Service
            {
                Name = name,
                Description = description
            };
            var createdService = serviceApi.PostService(service).Result;
        }

    }
}
