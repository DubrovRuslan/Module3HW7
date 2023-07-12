using System.Text;
using Module3HW7.Models;
using Module3HW7.Services.Abstractions;

namespace Module3HW7.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;
        private Config _config;
        private StringBuilder _log;
        private static int _logNumber = 0;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        public LoggerService(IConfigService configService, IFileService fileService)
        {
            _configService = configService;
            _fileService = fileService;
            _config = _configService.ReadConfigAsync().GetAwaiter().GetResult();
            _log = new StringBuilder();
            _fileService.ClearOldBackupDirectoryOrCreate();
        }

        public event Func<string, Task> CreateBackup;

        public async Task WriteLogAsync(LogType type, string text)
        {
            await _semaphoreSlim.WaitAsync();
            _logNumber++;
            var newLogItem = $"Log #{_logNumber} - {type.ToString()}: {text}";
            await _fileService.WriteAsync(newLogItem);
            _log.AppendLine(newLogItem);
            await Backup();
            _semaphoreSlim.Release();
        }

        public async Task Backup()
        {
            if (_logNumber % _config.LogCountBackup_N == 0)
            {
                _log.AppendLine();
                await CreateBackup?.Invoke(_log.ToString());
            }
        }

        public async Task LogErrorAsync(string message)
        {
            await WriteLogAsync(LogType.Error, message);
        }

        public async Task LogInfoAsync(string message)
        {
            await WriteLogAsync(LogType.Info, message);
        }

        public async Task LogWarningAsync(string message)
        {
            await WriteLogAsync(LogType.Warning, message);
        }
    }
}
