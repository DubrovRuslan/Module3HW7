using Module3HW7.Models;
using Module3HW7.Services.Abstractions;

namespace Module3HW7.Services
{
    public class FileService : IFileService
    {
        private const string FileName = "Log";
        private readonly Config _config;
        private SemaphoreSlim _semaphoreSlim;
        private StreamWriter _streamWriter;
        public FileService(IConfigService configService)
        {
            _config = configService.ReadConfigAsync().GetAwaiter().GetResult();
            _semaphoreSlim = new SemaphoreSlim(1);
            _streamWriter = new StreamWriter($"{FileName}{_config.FileExtension}");
        }

        public void ClearOldBackupDirectoryOrCreate()
        {
            try
            {
                var directoryInfo = new DirectoryInfo(_config.BackUpDirectotyPath);
                if (directoryInfo.Exists)
                {
                    var files = directoryInfo.GetFiles();
                    foreach (var item in files)
                    {
                        item.Delete();
                    }
                }
                else
                {
                    directoryInfo.Create();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task WriteAsync(string text)
        {
            await _semaphoreSlim.WaitAsync();
            await _streamWriter.WriteLineAsync(text);
            await _streamWriter.FlushAsync();
            _semaphoreSlim.Release();
        }
    }
}
