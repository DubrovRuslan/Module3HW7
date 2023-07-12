using Module3HW7.Models;
using Module3HW7.Services.Abstractions;

namespace Module3HW7.Services
{
    public class BackupService : IBackupService
    {
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private StreamWriter _streamWriter;
        private Config _config;
        public BackupService(IConfigService configService)
        {
            _config = configService.ReadConfigAsync().Result;
            var fileName = $"{_config.BackUpDirectotyPath}/bac - {DateTime.UtcNow.ToString(_config.TimeFormat)}{_config.FileExtension}";
            _streamWriter = new StreamWriter(fileName);
        }

        public async Task CreateBackupAsync(string text)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                await _streamWriter.WriteAsync(text);
                await _streamWriter.FlushAsync();
                await _streamWriter.DisposeAsync();
                _semaphoreSlim.Release();
            }
            catch (Exception)
            {
               return;
            }
        }
    }
}