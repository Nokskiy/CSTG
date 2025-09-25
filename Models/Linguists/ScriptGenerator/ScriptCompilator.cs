using System.Text.RegularExpressions;
using CSTG.Models.Linguists.ScriptGenerator.Converters;
using Microsoft.Extensions.Logging;

namespace CSTG.Models.Linguists.ScriptGenerator;

public class ScriptCompilator(ILogger logger, string outputPath) : IScriptCompilator
{
    private ILogger Logger => logger;
    public void Compilate(string inputFile)
    {
        var converter = new BashConverter(logger);
        
        Directory.CreateDirectory("output");
        var outputFilePath = Path.Combine(outputPath,Path.GetFileNameWithoutExtension(inputFile) + ".bash");
        var outputLines = converter.ConvetedFileLines(inputFile);
        File.Create(outputFilePath).Close();
        File.WriteAllLines(outputFilePath,outputLines);
    }
}