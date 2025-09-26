using CSTG.Models.Linguists;
using CSTG.Models.ProjectFilesManager;
using CSTG.Servises;
using Microsoft.Extensions.Logging;

namespace CSTG;

internal static class Program
{
    private static void Main(string[] args)
    {
        DI.Init();
        DI.GetService<IProjectFilesManager>().Compile();
    }
}