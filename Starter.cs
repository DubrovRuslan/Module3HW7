using Module3HW7.Services;
using Module3HW7.Services.Abstractions;

namespace Module3HW7
{
    public class Starter
    {
        private readonly ILoggerService _loggerService;
        private readonly IConfigService _configService;
        private TaskCompletionSource _tcs1 = new TaskCompletionSource();
        private TaskCompletionSource _tcs2 = new TaskCompletionSource();
        public Starter(ILoggerService loggerService, IConfigService configService)
        {
            _loggerService = loggerService;
            _configService = configService;
        }

        public void Run()
        {
            _loggerService.CreateBackup += CreateBackupAsync;
            Task.Run(async () => await Start(1, 50, _tcs1));
            Task.Run(async () => await Start(2, 50, _tcs2));
            _tcs1.Task.GetAwaiter().GetResult();
            _tcs2.Task.GetAwaiter().GetResult();
        }

        private async Task Start(int indexSesion, int count, TaskCompletionSource taskCompletionSource)
        {
            for (var i = 0; i < count; i++)
            {
                var rand = new Random();
                switch (rand.Next(4))
                {
                    case 0:
                        await _loggerService.LogInfoAsync($"In Session {indexSesion}, user {i} login");
                        break;
                    case 1:
                        await _loggerService.LogInfoAsync($"In Session {indexSesion}, user {i} logout");
                        break;
                    case 2:
                        await _loggerService.LogWarningAsync($"In Session {indexSesion}, user {i} access denied");
                        break;
                    case 3:
                        await _loggerService.LogErrorAsync($"In Session {indexSesion}, user {i} access error");
                        break;
                }
            }

            taskCompletionSource.SetResult();
        }

        private async Task CreateBackupAsync(string text)
        {
            await new BackupService(_configService).CreateBackupAsync(text);
        }
    }
}
