using Microsoft.Extensions.DependencyInjection;
using Module3HW7;
using Module3HW7.Services;
using Module3HW7.Services.Abstractions;

var serviceProvider = new ServiceCollection()
                .AddTransient<IConfigService, ConfigService>()
                .AddSingleton<IBackupService, BackupService>()
                .AddSingleton<ILoggerService, LoggerService>()
                .AddTransient<IFileService, FileService>()
                .AddTransient<Starter>()
                .BuildServiceProvider();
var start = serviceProvider.GetService<Starter>();
start.Run();
Console.WriteLine("Done");
Console.ReadKey();
