using CSTG.Models.Linguists;
using CSTG.Models.Linguists.ScriptGenerator;
using CSTG.Models.ProjectFilesManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NeoSimpleLogger;

namespace CSTG.Servises;

public static class DI
{
    private static IServiceProvider? ServiceProvider { get; set; }

    public static void Init()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices(services =>
        {
            services.AddTransient<IProjectFilesManager, ProjectFilesManager>();
            services.AddSingleton<ILogger, Logger>();
            services.AddTransient<IConfigFileLinguist, ConfigFileLinguist>();
            services.AddTransient<IScriptCompilator, ScriptCompilator>();
        }).ConfigureLogging(log =>
        {
            log.ClearProviders();
            log.AddProvider(new LoggerProvider());
        });

        var hostBuilder = host.Build();
        ServiceProvider = hostBuilder.Services;
    }

    public static T GetService<T>()
    {
        if (ServiceProvider == null)
            throw new InvalidOperationException("DI не инициализирован. Сначала вызовите Init()");
        return ServiceProvider.GetRequiredService<T>();
    }
}