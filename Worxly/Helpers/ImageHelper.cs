using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Worxly.Helpers
{
    public static class ImageHelper
    {
        public static string ConvertImageToBase64(string _imagePath)
        {
            string _base64String = null;

            using (System.Drawing.Image _image = System.Drawing.Image.FromFile(_imagePath))
            {
                using (MemoryStream _mStream = new MemoryStream())
                {
                    _image.Save(_mStream, _image.RawFormat);
                    byte[] _imageBytes = _mStream.ToArray();
                    _base64String = Convert.ToBase64String(_imageBytes);

                    return "data:image/jpg;base64," + _base64String;
                }
            }
        }

        public static async Task<Avalonia.Media.Imaging.Bitmap?> LoadFromWeb(string url)
        {
            if (url == null)
                return new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(new Uri("avares://Worxly/Assets/avalonia-logo.ico")));
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return new Avalonia.Media.Imaging.Bitmap(new MemoryStream(data));
            }
            catch (HttpRequestException ex)
            {
                return new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(new Uri("avares://Worxly/Assets/avalonia-logo.ico")));
            }
        }

    }
}
