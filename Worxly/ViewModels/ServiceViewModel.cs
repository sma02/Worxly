using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.Models;

namespace Worxly.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<Service> Services => services;
        public ReactiveCommand<Unit, Unit> AddButtonCommand { get; set; }
        private SourceCache<Service, string> sourceCache = new(x => x.Id);
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
            var services_e = new List<Service>
{
    new Service { Id = "1", Name = "Bulldozer", Description = "A bulldozer is a large and heavy tractor equipped with a substantial metal plate used to push large quantities of soil, sand, rubble, or other material during construction or conversion work." },
    new Service { Id = "2", Name = "Bulb Fixer", Description = "Specializes in fixing high-quality bulbs efficiently and safely." },
    new Service { Id = "3", Name = "Plumber", Description = "Provides plumbing services for residential and commercial buildings, handling pipe repairs, installations, and maintenance." },
    new Service { Id = "4", Name = "Electrician", Description = "Expert in wiring, repairs, and installing electrical systems and appliances in various settings." },
    new Service { Id = "5", Name = "Painter", Description = "Offers interior and exterior painting services with attention to detail and precision." },
    new Service { Id = "6", Name = "Carpenter", Description = "Crafts and repairs wooden furniture, fittings, and other carpentry-related items." },
    new Service { Id = "7", Name = "Lawn Care", Description = "Provides lawn mowing, trimming, and garden maintenance for a well-kept yard." },
    new Service { Id = "8", Name = "Roofing", Description = "Specializes in repairing and installing roofs of various materials for both homes and businesses." },
    new Service { Id = "9", Name = "Window Cleaning", Description = "Professional window cleaning service for high-rise and residential buildings." },
    new Service { Id = "10", Name = "HVAC Technician", Description = "Experienced in HVAC system repairs, installations, and maintenance to ensure efficient cooling and heating." },
    new Service { Id = "11", Name = "Pest Control", Description = "Eliminates common household pests and provides preventive solutions for long-term control." },
    new Service { Id = "12", Name = "Pool Cleaning", Description = "Maintains and cleans swimming pools, ensuring balanced water chemistry and sanitation." },
    new Service { Id = "13", Name = "Housekeeping", Description = "General cleaning and maintenance services for homes and apartments." },
    new Service { Id = "14", Name = "Security Services", Description = "Provides security personnel for various events, residential, and commercial properties." },
    new Service { Id = "15", Name = "Mover", Description = "Assists with packing, loading, and unloading belongings during relocations." },
    new Service { Id = "16", Name = "Mechanic", Description = "Skilled in automotive repairs, maintenance, and part replacements for all vehicle types." },
    new Service { Id = "17", Name = "IT Support", Description = "Offers technical support and troubleshooting for computers, networks, and software." },
    new Service { Id = "18", Name = "Gardener", Description = "Provides gardening and landscaping services for residential and commercial properties." },
    new Service { Id = "19", Name = "Dog Walker", Description = "Professional dog-walking services with a focus on pet safety and exercise." },
    new Service { Id = "20", Name = "Personal Trainer", Description = "Certified trainer offering fitness and wellness plans tailored to individual needs." }
};
            sourceCache.Edit(updater => updater.AddOrUpdate(services_e));
            sourceCache.Connect()
                .Filter(x => x.Name.ToLower().Contains(searchText.ToLower())).Bind(out services)
                .Sort(SortExpressionComparer<Service>.Ascending(t => t.Name))
                .Subscribe();
            AddButtonCommand = ReactiveCommand.Create(AddButtonClick);
        }
        public void AddButtonClick()
        {
            throw new NotImplementedException();
        }
    }
}
