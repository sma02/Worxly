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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private string? imageFile;

        public string? ImageFile
        {
            get => imageFile;
            set
            {
                if (imageFile == value) return;
                imageFile = value;
                OnPropertyChanged(nameof(imageFile));
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
            Image = await Helpers.ImageHelper.LoadImageFromServer(ImageFile);
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