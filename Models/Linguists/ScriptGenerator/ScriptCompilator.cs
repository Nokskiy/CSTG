using System.Text.RegularExpressions;
using CSTG.Models.Linguists.ScriptGenerator.Converters;
using Microsoft.Extensions.Logging;
using FilesManager = CSTG.Models.ProjectFilesManager.ProjectFilesManager;

namespace CSTG.Models.Linguists.ScriptGenerator;

public class ScriptCompilator(ILogger logger) : IScriptCompilator
{
    private ILogger Logger => logger;
    public void Compilate(string inputFile)
    {
        var converter = new BashConverter(logger);
        
        Directory.CreateDirectory("output");
        var outputFilePath = Path.Combine(FilesManager.OutputFilesPath ,Path.GetFileNameWithoutExtension(inputFile) + ".bash");
        var outputLines = converter.ConvetedFileLines(inputFile);
        File.Create(outputFilePath).Close();
        File.WriteAllLines(outputFilePath,outputLines);
    }
}