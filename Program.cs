using Microsoft.Extensions.Logging;

namespace CSTG;

internal static class Program
{
    private static DI _di = new();

    private static void Main(string[] args)
    {
        _di.Init();
    }
}