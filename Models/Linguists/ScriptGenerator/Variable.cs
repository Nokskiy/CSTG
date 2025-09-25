using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace CSTG.Models.Linguists.ScriptGenerator;

public struct Variable(VariableTypes type, string name, string value)
{
    public VariableTypes Type => type;
    public string Name => name;
    public string Value => value;

    public static bool IsVariable(string text)
    {
        var pattern = @"^([^\s]+)\s+([^=]+)=([^\s]*)$";
        var splited = Regex.Match(text, pattern);
        return splited.Groups.Count >= 3;
    }
    public static bool IsVariableType(string name) =>
        Enum.GetValues<VariableTypes>().Any(type => type.ToString() == name);
    
    public static Variable ConvertStringToVariableStruct(ILogger logger, string expression)
    {
        var pattern = @"^([^\s]+)\s+([^=]+)=([^\s]*)$";
        
        var splited = Regex.Match(expression, pattern);
        
        var type = splited.Groups[1].Value.Trim();

        if (!IsVariableType(type))
        {
            logger.LogError($"Invalid variable type {type}");
            Environment.Exit(1);
        }
        
        return new Variable(Enum.Parse<VariableTypes>(type), splited.Groups[2].Value, splited.Groups[3].Value);
    }
    
    public override string ToString()
    {
        return $"{Name}={Value}";
    }
}