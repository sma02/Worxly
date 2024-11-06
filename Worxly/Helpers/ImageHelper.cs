using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Worxly.Helpers
{
    public static class ImageHelper
    {
        public static async Task<Bitmap?> LoadFromWeb(string url)
        {
            if (url == null)
                return new Bitmap(AssetLoader.Open(new Uri("avares://Worxly/Assets/avalonia-logo.ico")));
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return new Bitmap(new MemoryStream(data));
            }
            catch (HttpRequestException ex)
            {
                return new Bitmap(AssetLoader.Open(new Uri("avares://Worxly/Assets/avalonia-logo.ico")));
            }
        }
    }
}
