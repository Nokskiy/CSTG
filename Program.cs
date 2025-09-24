using CSTG.Models.Linguists;
using CSTG.Models.ProjectFilesManager;
using CSTG.Servises;
using Microsoft.Extensions.Logging;

namespace CSTG;

internal static class Program
{
    private static readonly DI Di = new();

    private static void Main(string[] args)
    {
        Di.Init();
        //new ProjectFilesManager(Di.GetService<ILogger>(),Di.GetService<IConfigFileLinguist>()).Init("TestProject");
        new ProjectFilesManager(Di.GetService<ILogger>(),Di.GetService<IConfigFileLinguist>()).Compile();
    }
}