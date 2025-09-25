using Microsoft.Extensions.Logging;

namespace CSTG.Models.Linguists.ScriptGenerator.Converters;

public class Converter(ILogger logger)
{
    protected virtual string? OutputLine(Variable variable, string? comment) => string.Empty;

    protected virtual string? ConvertedToNumber(Variable convertedVariable) => string.Empty;

    protected virtual string ConvertedToString(Variable convertedVariable) => string.Empty;

    protected virtual string ConvertedComment(string comment) => string.Empty;
}