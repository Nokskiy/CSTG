using CSTG.ConfigVariable;

namespace CSTG.Models.Linguists;

public interface IConfigFileLinguist
{
    string CreateVariable(VariableTypes type, string value);
    Variable ReadVariable(string text);
    bool IsVariableType(string name);
}