using CSTG.Models.Linguists;
using CSTG.Models.ProjectFilesManager;
using CSTG.Servises;
using Microsoft.Extensions.Logging;

namespace CSTG;

internal static class Program
{
    private static DI _di = new();

    private static void Main(string[] args)
    {
        _di.Init();
        new ProjectFilesManager(_di.GetService<ILogger>(),_di.GetService<IConfigFileLinguist>()).Init("Test project");
    }
}