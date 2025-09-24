using CSTG.ConfigVariable;
using CSTG.Models.Linguists;
using CSTG.Models.Linguists.ScriptGenerator;
using Microsoft.Extensions.Logging;
using VariableTypes = CSTG.ConfigVariable.VariableTypes;

namespace CSTG.Models.ProjectFilesManager;

public class ProjectFilesManager(ILogger logger, IConfigFileLinguist linguist) : IProjectFilesManager
{
    private IConfigFileLinguist Linguist => linguist;
    private ILogger Logger => logger;
    private static string ProjectConfigExtension => ".cstg_cfg";
    private static readonly string InputFilesPath = Path.Combine(Environment.CurrentDirectory, "input");
    private static readonly string OutputFilesPath = Path.Combine(Environment.CurrentDirectory, "output");

    public void Init(string name)
    {
        var cfgFilePath = Path.Combine(Environment.CurrentDirectory, ProjectConfigExtension);
        File.Create(cfgFilePath).Close();
        File.WriteAllText(cfgFilePath, Linguist.CreateVariable(VariableTypes.ProjectName, name));
        Directory.CreateDirectory(InputFilesPath);
        File.Create(Path.Combine(InputFilesPath, "main.txt")).Close();
    }

    public void Compile()
    {
        foreach (var file in Directory.GetFiles(InputFilesPath))
        {
            new ScriptCompilator(Logger, OutputFilesPath).Compilate(file);
        }
    }
}