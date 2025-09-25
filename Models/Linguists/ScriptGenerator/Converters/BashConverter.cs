using Microsoft.Extensions.Logging;

namespace CSTG.Models.Linguists.ScriptGenerator.Converters;

public class BashConverter(ILogger logger) : Converter(logger)
{
    private readonly ILogger _logger = logger;
    
    public List<string> ConvetedFileLines(string inputFile)
    {
        List<string> outputScriptLines = [];

        for (var lineIndex = 0; lineIndex < File.ReadAllLines(inputFile).Length; lineIndex++)
        {
            var rawLine = File.ReadAllLines(inputFile)[lineIndex];
            var line = new Line(Line.GetExpressionFromText(rawLine), Line.GetCommentFromText(rawLine));
            
            if (Variable.IsVariable(line.Expression))
            {
                var output = OutputLine(Variable.ConvertStringToVariableStruct(logger,line.Expression),
                    line.Comment);
                if (output != null)
                {
                    outputScriptLines.Add(output);
                    _logger.LogInformation($"line {rawLine}, {output}");
                }
            }
        }
        
        return outputScriptLines;
    }

    protected override string? OutputLine(Variable variable, string? comment)
    {
        switch (variable.Type)
        {
            case VariableTypes.str:
                return ConvertedToString(variable);
            case VariableTypes.number:
                return ConvertedToNumber(variable);
            default:
                _logger.LogError($"Unknown variable type: {variable.Type}");
                Environment.Exit(1);
                return null;
        }
    }

    protected override string? ConvertedToNumber(Variable convertedVariable)
    {
        if (int.TryParse(convertedVariable.Value, out var parsed))
        {
            return $"{convertedVariable.Name}={convertedVariable.Value}";
        }
        _logger.LogError($"line: {convertedVariable.Value}, {convertedVariable.Value} can't be parsed to number");
        Environment.Exit(1);
        return null;
    }
    
    protected override string ConvertedToString(Variable convertedVariable) =>
        $"{convertedVariable.Name}=\"{convertedVariable.Value}\"";
}