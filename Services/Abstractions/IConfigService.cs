using Module3HW7.Models;

namespace Module3HW7.Services.Abstractions
{
    public interface IConfigService
    {
        public Task<Config> ReadConfigAsync();
    }
}
