using Module3HW7.Models;
using Module3HW7.Services.Abstractions;
using Newtonsoft.Json;

namespace Module3HW7.Services
{
    public class ConfigService : IConfigService
    {
        private const string ConfigPath = "Config.json";
        public async Task<Config> ReadConfigAsync()
        {
            try
            {
                var configFile = await File.ReadAllTextAsync(ConfigPath);
                var config = JsonConvert.DeserializeObject<Config>(configFile);
                return config;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
