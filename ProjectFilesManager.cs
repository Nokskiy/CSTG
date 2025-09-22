using System.Text.RegularExpressions;
using CSTG.ConfigVariable;

namespace CSTG;

public class ProjectFilesManager
{
    private static string ProjectConfigExtention => ".cstg_cfg";

    public void Init(string name)
    {
        var cfgFilePath = Path.Combine(Environment.CurrentDirectory, ProjectConfigExtention);
        File.Create(cfgFilePath).Close();
        File.WriteAllText(cfgFilePath, MakeVariable(VariableTypes.ProjectName, name));
    }

    private static string MakeVariable(VariableTypes type, string value) =>
        type + ":" + value;

    private static Variable ReadVariable(string text)
    {
        if (text == String.Empty)
        {
            Console.WriteLine("Text is empty");
            Environment.Exit(1);
        }
        
        var splited = Regex.Match(text, @"^(.*?):(.*)$");

        if (splited.Groups.Count < 2)
        {
            Console.WriteLine("Invalid format");
            Environment.Exit(1);
        }

        if (!IsVariableType(splited.Groups[1].Value.Trim()))
        {
            Console.WriteLine("Invalid variable type");
            Environment.Exit(1);
        }
            
        return new Variable(Enum.Parse<VariableTypes>(splited.Groups[1].Value.Trim()),
            splited.Groups[2].Value.Trim());
    }

    private static bool IsVariableType(string name) =>
        Enum.GetValues<VariableTypes>().Any(type => type.ToString() == name);
}