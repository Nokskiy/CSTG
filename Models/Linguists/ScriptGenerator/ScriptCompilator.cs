using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace CSTG.Models.Linguists.ScriptGenerator;

public class ScriptCompilator(ILogger logger, string outputPath) : IScriptCompilator
{
    private ILogger Logger => logger;
    public void Compilate(string inputFile)
    {
        List<string> outputScriptLines = [];
        foreach (var line in File.ReadAllLines(inputFile))
        {
            if (line.StartsWith("//"))
            {
                logger.LogInformation($"line: {line}, is comment");
                continue;
            }
            if (IsVariable(line))
            {
                var convertedVariable = ConvertVariable(line);
                if(convertedVariable.Type == VariableTypes.str)
                    outputScriptLines.Add($"{convertedVariable.Name}=\"{convertedVariable.Value}\"");
                else if (convertedVariable.Type == VariableTypes.number)
                    if (int.TryParse(convertedVariable.Value, out var parsed))
                    {
                        outputScriptLines.Add($"{convertedVariable.Name}={convertedVariable.Value}");
                    }
                    else
                    {
                        logger.LogError($"line: {line}, {convertedVariable.Value} can't be parsed to number");
                        Environment.Exit(1);
                    }
                logger.LogInformation($"line: {line}, converted variable: {convertedVariable.ToString()}");
            }
        }
        
        Directory.CreateDirectory("output");
        var outputFilePath = Path.Combine(outputPath,Path.GetFileNameWithoutExtension(inputFile) + ".bash");
        File.Create(outputFilePath).Close();
        File.WriteAllLines(outputFilePath,outputScriptLines);
        
    }

    public bool IsVariable(string text)
    {
        var pattern = @"^([^\s]+)\s+([^=]+)=([^\s]*)$";
        var splited = Regex.Match(text, pattern);
        return splited.Groups.Count >= 3;
    }

    public Variable ConvertVariable(string text)
    {
        var pattern = @"^([^\s]+)\s+([^=]+)=([^\s]*)$";
        
        var splited = Regex.Match(text, pattern);
        
        return new Variable(Enum.Parse<VariableTypes>(splited.Groups[1].Value.Trim()), splited.Groups[2].Value, splited.Groups[3].Value);
    }
    
    public bool IsVariableType(string name) =>
        Enum.GetValues<VariableTypes>().Any(type => type.ToString() == name);
}