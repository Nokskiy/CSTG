using CSTG.Models.Linguists;
using CSTG.Models.ProjectFilesManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NeoSimpleLogger;

namespace CSTG.Servises;

public class DI
{
    private static IServiceProvider? ServiceProvider { get; set; }

    public void Init()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices(services =>
        {
            services.AddSingleton<ILogger, Logger>();
            services.AddTransient<IProjectFilesManager, ProjectFilesManager>();
            services.AddTransient<IConfigFileLinguist, ConfigFileLinguist>();
        }).ConfigureLogging(log =>
        {
            log.ClearProviders();
            log.AddProvider(new LoggerProvider());
        });

        var hostBuilder = host.Build();
        ServiceProvider = hostBuilder.Services;
    }

    public T GetService<T>()
    {
        if (ServiceProvider == null)
            throw new InvalidOperationException("DI не инициализирован. Сначала вызовите Init()");
        return ServiceProvider.GetRequiredService<T>();
    }
}