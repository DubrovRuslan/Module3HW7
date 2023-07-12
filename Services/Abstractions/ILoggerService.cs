using Module3HW7.Models;

namespace Module3HW7.Services.Abstractions
{
    public interface ILoggerService
    {
        public event Func<string, Task> CreateBackup;
        public Task WriteLogAsync(LogType type, string text);
        public Task LogErrorAsync(string message);
        public Task LogInfoAsync(string message);
        public Task LogWarningAsync(string message);
    }
}
