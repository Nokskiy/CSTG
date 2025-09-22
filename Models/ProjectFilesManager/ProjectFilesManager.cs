using CSTG.ConfigVariable;
using CSTG.Models.Linguists;
using Microsoft.Extensions.Logging;

namespace CSTG.Models.ProjectFilesManager;

public class ProjectFilesManager(ILogger logger, IConfigFileLinguist linguist) : IProjectFilesManager
{
    private IConfigFileLinguist Linguist => linguist;
    private ILogger Logger => logger;
    private static string ProjectConfigExtention => ".cstg_cfg";

    public void Init(string name)
    {
        var cfgFilePath = Path.Combine(Environment.CurrentDirectory, ProjectConfigExtention);
        File.Create(cfgFilePath).Close();
        File.WriteAllText(cfgFilePath, Linguist.MakeVariable(VariableTypes.ProjectName, name));
    }
}