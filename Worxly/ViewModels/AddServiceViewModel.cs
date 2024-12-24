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
using Worxly.Helpers;

namespace Worxly.ViewModels
{
    public class AddServiceViewModel : ViewModelBase
    {
        private Service service = null!;
        private string currentImageFile;

        public ICommand AddServiceCommand { get; }
        public Service Service { get => service; set => this.RaiseAndSetIfChanged(ref service, value); }
        public string CurrentImageFile
        {
            get => currentImageFile; set
            {
                currentImageFile = value;
                if (Service != null)
                    Service.ImageFile = value;
            }
        }
        public AddServiceViewModel(Service? service = null)
        {
            EditMode = service != null;
            Service = service ?? new Service();
            AddServiceCommand = ReactiveCommand.Create(AddServiceClick);
        }
        public async void AddServiceClick()
        {
            var serviceApi = RestService.For<IServiceApi>(Properties.Resources.DefaultHost);
            if(service.ImageFile==null)
                service.ImageFile = "";
            if (EditMode)
            {
                await serviceApi.PutService(Service.Id, Service);
                //serviceApi.DeleteService(Service.Id);
                
            }
            else
            {
                var createdService = await serviceApi.PostService(service);
            }
        }

    }
}
    