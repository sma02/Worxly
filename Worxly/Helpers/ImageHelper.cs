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
using Avalonia.Platform.Storage;
using Mapsui.Extensions;
using Refit;
using Worxly.Api;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Worxly.Helpers
{
    public class ImageResponse
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; }
    }
    public static class ImageHelper
    {
        public static async Task<string> ConvertImageToBase64(IStorageFile file)
        {
            string _base64String = null;

            var stream = await file.OpenReadAsync();
            byte[] _imageBytes = stream.ToBytes();
            _base64String = Convert.ToBase64String(_imageBytes);

            return "data:image/jpg;base64," + _base64String;
        }
        public static async Task<string?> UploadImage(IStorageFile file)
        {
            var httpClient = new HttpClient();
            string extension = Path.GetExtension(file.Path.LocalPath);
            Refit.StreamPart stream = new StreamPart(await file.OpenReadAsync(),$"file{extension}");
            var imageApi = RestService.For<IImageApi>(Properties.Resources.DefaultHost);
            var res = await imageApi.UploadImage(stream);
            if (res.Content is not null)
            {
                var imageFile = JsonSerializer.Deserialize<ImageResponse>(res.Content);
                if (imageFile is not null)
                    return imageFile.Filename;
            }
            return null;
        }
        public static async Task<Avalonia.Media.Imaging.Bitmap?> LoadFromWeb(string url)
        {
            if (url == null)
                return new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(new Uri("avares://Worxly/Assets/avalonia-logo.ico")));
            var httpClient = new HttpClient();
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
        public static async Task<Avalonia.Media.Imaging.Bitmap?> LoadImageFromServer(string filename)
        {
            return await LoadFromWeb($"{Properties.Resources.DefaultHost}/api/image/{filename}");
        }
    }
}
