using System.Text.RegularExpressions;
using CSTG.ConfigVariable;
using Microsoft.Extensions.Logging;

namespace CSTG.Models.Linguists;

public class ConfigFileLinguist(ILogger logger) : IConfigFileLinguist
{
    private ILogger Logger => logger;
    
    public string CreateVariable(VariableTypes type, string value) =>
        type + "=" + value;
    
    public Variable ReadVariable(string text)
    {
        if (text == String.Empty)
        {
            Logger.LogError("Text is empty");
            Environment.Exit(1);
        }
        
        var splited = Regex.Match(text, @"^(.*?):(.*)$");

        if (splited.Groups.Count < 2)
        {
            Logger.LogError("Invalid format");
            Environment.Exit(1);
        }

        if (!IsVariableType(splited.Groups[1].Value.Trim()))
        {
            Logger.LogError("Invalid variable type");
            Environment.Exit(1);
        }
            
        return new Variable(Enum.Parse<VariableTypes>(splited.Groups[1].Value.Trim()),
            splited.Groups[2].Value.Trim());
    }

    public bool IsVariableType(string name) =>
        Enum.GetValues<VariableTypes>().Any(type => type.ToString() == name);
}