using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Worxly.DTOs;

namespace Worxly.Helpers
{
    public class AppConfig
    {
        private static readonly Lazy<AppConfig> _instance = new Lazy<AppConfig>(() => LoadConfig());
        public static AppConfig Instance => _instance.Value;
        private static string basePath = Globals.Instance.DataDirectory;
        private static string filename = "appconfig.json";
        public User? User { get; set; }
        public static AppConfig LoadConfig()
        {
            string basePath = Globals.Instance.DataDirectory;
            string path = Path.Join(basePath, filename);
            if (!File.Exists(path))
            {
                return new AppConfig();
            }
            else
            {
                return JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(path));
            }
        }
        public static void SaveConfig()
        {
            User? user = null;
            if( Globals.Instance.UserPresistence)
             user = Instance.User;
            var data = new {
                User = user
            };
            string basePath = Globals.Instance.DataDirectory;
            string path = Path.Join(basePath, filename);
            File.WriteAllText(path, JsonSerializer.Serialize(data));
        }
    }
}
