using CSTG;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NeoSimpleLogger;

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