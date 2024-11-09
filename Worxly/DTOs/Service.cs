using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worxly.DTOs
{
    public class Service : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private string? imageUrl;

        public string? ImageUrl
        {
            get => imageUrl; 
            set
            {
                if(imageUrl == value) return;
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
                LoadImage();
            }
        }
        private Bitmap? image;
        public Bitmap? Image
        {
            get => image; 
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        public async void LoadImage()
        {
            Image = await Helpers.ImageHelper.LoadFromWeb(ImageUrl);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Service()
        {
            LoadImage();
        }
    }
}
